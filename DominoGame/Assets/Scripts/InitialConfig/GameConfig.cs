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
        strats = Implementations.GetStrategies<T>();
        evals = Implementations.GetEvaluators<T>();
        criteria = Implementations.GetCriteria<T>();
        filters = Implementations.GetFilters<T>();
        effects = Implementations.GetEffects<T>();

        Evaluators.UpdateNames(TypesNameHandler.GetNames<evaluator<T>>(evals));
        PlayersOptionsContainer.UpdateImplementations(TypesNameHandler.GetNames<strategy<T>>(strats));
        ManagersPowerFilters.GetImplementationData(TypesNameHandler.GetNames<Action<EffectsExecution<T>>>(effects), TypesNameHandler.GetNames<tokenFilter<T>>(filters));
        VictoryContainer.UpdateImplementations(TypesNameHandler.GetNames<victoryCriteria<T>>(criteria));

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
        evaluator<T> evaluator = TypesNameHandler.ImplementationByName<evaluator<T>>(evals, Evaluators.Current);

        // Getting player strategies
        string[] playerStrategies = PlayersOptionsContainer.Currents;
        
        if (playerStrategies.Length == 0) {
            ShowInputError("Player amount can't be zero");
            return;
        }

        strategy<T>[] strategies = new strategy<T>[playerStrategies.Length];
        for (int i = 0; i < playerStrategies.Length; i++) {
            strategies[i] = TypesNameHandler.ImplementationByName<strategy<T>>(strats, playerStrategies[i]);
        }

        // Getting player names
        TMP_InputField[] nameInputs = PlayersOptionsContainer.gameObject.GetComponentsInChildren<TMP_InputField>();
        string[] playerNames = new string[nameInputs.Length];

        for (int i = 0; i < nameInputs.Length; i++) {
            
            playerNames[i] = (nameInputs[i].text != "" ? nameInputs[i].text : "Player #" + (i + 1));
        }

        // Getting powers
        Tuple<string, string>[] powers = ManagersPowerFilters.GetPowers;
        Power<T>[] powerCollection = new Power<T>[powers.Length];

        for (int i = 0; i < powers.Length; i++) {
            
            var currentEffect = TypesNameHandler.ImplementationByName<Action<EffectsExecution<T>>>(effects, powers[i].Item1);
            var currentFilter = TypesNameHandler.ImplementationByName<tokenFilter<T>>(filters, powers[i].Item2);
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

            var currentCriteria = TypesNameHandler.ImplementationByName<victoryCriteria<T>>(criteria, criteriaNames[i]);
            var currentValue = (inputValues[i].text != "" ? int.Parse(inputValues[i].text) : 0);
            victoryCheckers[i] = new VictoryChecker<T>(currentCriteria, currentValue);
        }

        try {
            // Instantiating Game Manager
            GameManager<T> managerInstance = new GameManager<T>(
                strategies: strategies,
                generator: (Generator<T>)generator,
                tokenTypeAmount: amountOfOutputTypes,
                tokensInHand: tokensPerHand,
                outputsAmount: outputsPerToken,
                playerNames: playerNames,
                evaluator: evaluator,
                victoryCheckerCollection: new CriteriaCollection<T>(victoryCheckers),
                powers: new Powers<T>(powerCollection)
            );

            ConfigPanel.SetActive(false);
            GetComponent<GameFlow>().StartGame<T>(managerInstance);
        }
        catch(Exception e) {
            if (e.InnerException.Message.Contains("Can't deal")) {
                ShowInputError("Insuficient tokens to deal to each player");
            }
        }
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
