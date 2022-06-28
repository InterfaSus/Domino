using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DominoEngine;
using DominoEngine.Utils;

public class GameConfig : MonoBehaviour
{   

    public Slider OutputsPerToken;
    public Slider AmountOfOutputTypes;
    public TMP_InputField TokensPerHand;
    public GameObject ConfigPanel;
    public SelectorController Evaluators;
    public ScrollContentController PlayersOptionsContainer;
    public SelectorController GameManagers;
    public PowersScroll ManagersPowerFilters;
    public ScrollContentController VictoryContainer;
    public GameObject ErrorPanel;
    
    object[] managers;
    object[] strats;
    object[] evals;
    object[] filters;
    object[] effects;
    object[] criteria;
    object generator;

    public void GetImplementations<T>(Generator<T> generator) where T : IEvaluable {
        
        ConfigPanel.SetActive(true);

        this.generator = generator;
        managers = Implementations.GetGameManagers();
        strats = Implementations.GetStrategies<T>();
        evals = Implementations.GetEvaluators<T>();
        criteria = Implementations.GetCriteria<T>();
        filters = Implementations.GetFilters<T>();
        effects = Implementations.GetEffects<T>();

        Evaluators.UpdateNames(GetNames<evaluator<T>>(evals));
        PlayersOptionsContainer.UpdateImplementations(GetNames<strategy<T>>(strats));
        GameManagers.UpdateNames(GetNames<Type>(managers));
        ManagersPowerFilters.GetImplementationData(GetNames<Action<IGameManager<T>>>(effects), GetNames<tokenFilter<T>>(filters));
        VictoryContainer.UpdateImplementations(GetNames<victoryCriteria<T>>(criteria));

        FindObjectOfType<AcceptButton>().AddHandler<T>();
    }

    public void GetUserInputs<T>() where T : IEvaluable {

        // Getting numeric data
        int outputsPerToken = (int)this.OutputsPerToken.value;
        int amountOfOutputTypes = (int)this.AmountOfOutputTypes.value;

        if (this.TokensPerHand.text == "") {
            ShowInputError("Tokens per Hand field can't be blank");
            return;
        }

        int tokensPerHand = int.Parse(this.TokensPerHand.text);

        if (tokensPerHand < 1) {
            ShowInputError("Tokens per Hand value must be greater than zero");
            return;
        }

        // Getting evaluator
        evaluator<T> evaluator = ImplementationByName<evaluator<T>>(evals, Evaluators.Current);

        // Getting player strategies
        string[] playerStrategies = PlayersOptionsContainer.Currents;
        
        if (playerStrategies.Length == 0) {
            ShowInputError("Player amount can't be zero");
            return;
        }

        strategy<T>[] strategies = new strategy<T>[playerStrategies.Length];
        for (int i = 0; i < playerStrategies.Length; i++) {
            strategies[i] = ImplementationByName<strategy<T>>(strats, playerStrategies[i]);
        }

        // Getting player names
        TMP_InputField[] nameInputs = PlayersOptionsContainer.gameObject.GetComponentsInChildren<TMP_InputField>();
        string[] playerNames = new string[nameInputs.Length];

        for (int i = 0; i < nameInputs.Length; i++) {
            
            playerNames[i] = (nameInputs[i].text != "" ? nameInputs[i].text : "Player #" + (i + 1));
        }

        // Getting game manager
        Type gameManager = ImplementationByName<Type>(managers, GameManagers.Current);

        // Getting powers
        Tuple<string, string>[] powers = ManagersPowerFilters.GetPowers;
        Power<T>[] powerCollection = new Power<T>[powers.Length];

        for (int i = 0; i < powers.Length; i++) {
            
            var currentEffect = ImplementationByName<Action<IGameManager<T>>>(effects, powers[i].Item1);
            var currentFilter = ImplementationByName<tokenFilter<T>>(filters, powers[i].Item2);
            powerCollection[i] = new Power<T>(currentFilter, currentEffect);
        }

        // Getting victory criteria
        TMP_InputField[] inputValues = VictoryContainer.gameObject.GetComponentsInChildren<TMP_InputField>();
        string[] criteriaNames = VictoryContainer.Currents;
        if (criteriaNames.Length == 0) {
            ShowInputError("There must be at least one victory condition");
            return;
        }

        VictoryChecker<T>[] victoryCheckers = new VictoryChecker<T>[criteriaNames.Length];

        for (int i = 0; i < criteriaNames.Length; i++) {

            var currentCriteria = ImplementationByName<victoryCriteria<T>>(criteria, criteriaNames[i]);
            var currentValue = (inputValues[i].text != "" ? int.Parse(inputValues[i].text) : 0);
            victoryCheckers[i] = new VictoryChecker<T>(currentCriteria, currentValue);
        }

        try {
            // Intantiating Game Manager
            var genericManager = gameManager.MakeGenericType(typeof(T));
            var managerInstance = (IGameManager<T>)Activator.CreateInstance(genericManager, new object[] {
                strategies, // Strategies
                (Generator<T>)generator, // Generator
                amountOfOutputTypes, // tokenTypeAmount
                tokensPerHand, // tokensInHand
                outputsPerToken, // outputsAmount
                playerNames, // playerNames
                evaluator, // Evaluator OK
                new CriteriaCollection<T>(victoryCheckers), // VictoryCheckerCollection
                new Powers<T>(powerCollection), // Powers
            });

            ConfigPanel.SetActive(false);
            GetComponent<GameFlow>().StartGame<T>(managerInstance);
        }
        catch(Exception e) {
            if (e.InnerException.Message.Contains("Can't deal")) {
                ShowInputError("Insuficient tokens to deal to each player");
            }
        }
    }

    private string[] GetNames<T>(object[] tuples) {

        List<string> result = new List<string>();
        var collection = (Tuple<string, T>[])tuples;

        foreach (var item in collection) {
            result.Add(item.Item1);
        }

        return result.ToArray();
    }

    private T ImplementationByName<T>(object[] tuple, string name) {

        if (name == "None") return default(T);

        var collection = (Tuple<string, T>[])tuple;

        foreach (var item in collection) {
            if(item.Item1 == name) return item.Item2;
        }

        return default(T);
    }

    private void ShowInputError(string message) {

        ErrorPanel.SetActive(true);
        ErrorPanel.GetComponentsInChildren<TextMeshProUGUI>()[1].text = message;
    }

    private string[] AddNull(string[] names) {
        
        string[] result = new string[names.Length + 1];
        result[0] = "None";
        names.CopyTo(result, 1);

        return result;
    }
}
