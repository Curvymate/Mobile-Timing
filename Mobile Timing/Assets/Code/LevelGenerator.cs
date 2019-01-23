using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Rule initialRule;
    private Rule currentRule;
    private Rule previousRule;

    [SerializeField] private float spacing;
    private Vector2 pos;

    private SObject spawnedObject;
    private SObject previousObject;

    private Vector2 targetPosition;
    [SerializeField] private float horizontalOffset;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        targetPosition = Player.instance.Position;

        if (pos.x <= targetPosition.x + horizontalOffset)
            GenerateFromRuleset();
    }

    private void Init()
    {
        ChangeRule(initialRule);
        Generate(currentRule.sObject, Vector2.zero);
        ChangeRule(initialRule.link[Random.Range(0, initialRule.link.Length)]);
    }

    private void GenerateFromRuleset()
    {
        SObject currentObject = currentRule.sObject;
        float spacingX = 0;
        float spacingY = 0;

        if (previousRule.objectType != currentRule.objectType)
        {
            spacingX = Random.Range(currentRule.spacing.min.x, currentRule.spacing.max.x);
            spacingY = Random.Range(currentRule.spacing.min.y, currentRule.spacing.max.y);
        }

        Vector2 previousPlatformSize = spawnedObject.GetSize_Half();
        Vector2 currentPlatformSize = currentObject.GetSize_Half();

        pos.x += (previousPlatformSize.x + currentPlatformSize.x) + spacingX;
        pos.y += spacingY;

        Generate(currentObject, pos);

        ChangeRule(currentRule.link[Random.Range(0, currentRule.link.Length)]);
    }

    private void Generate(SObject _object, Vector2 pos)
    {
        this.pos = pos;
            
        spawnedObject = Instantiate(_object, pos, Quaternion.identity);

        spawnedObject.Init(Player.instance.movement);

        previousObject = spawnedObject;
    }

    private void ChangeRule(Rule rule)
    {
        previousRule = currentRule;
        currentRule = rule;
    }
}