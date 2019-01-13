using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Rule initialRule;
    private Rule currentRule;
    private Rule previousRule;

    [SerializeField] private float spacing;
    private Vector2 pos;

    private InteractableObstacle spawnedPlatform;
    private InteractableObstacle previousPlatform;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateFromRuleset();
    }

    private void Init()
    {
        ChangeRule(initialRule);
        SpawnPlatform(currentRule.platform, Vector2.zero);
        ChangeRule(initialRule.link[Random.Range(0, initialRule.link.Length)]);
    }

    private void GenerateFromRuleset()
    {
        InteractableObstacle currentPlatform = currentRule.platform;
        float spacingX = 0;
        float spacingY = 0;

        if (previousRule.objectType != currentRule.objectType)
        {
            spacingX = Random.Range(currentRule.spacing.min.x, currentRule.spacing.max.x);
            spacingY = Random.Range(currentRule.spacing.min.y, currentRule.spacing.max.y);
        }

        Vector2 previousPlatformSize = spawnedPlatform.GetSize();
        Vector2 currentPlatformSize = currentPlatform.GetSize();

        pos.x += (previousPlatformSize.x + currentPlatformSize.x) + spacingX;
        pos.y += spacingY;

        SpawnPlatform(currentPlatform, pos);

        ChangeRule(currentRule.link[Random.Range(0, currentRule.link.Length)]);
    }

    private void SpawnPlatform(Entity platform, Vector2 pos)
    {
        this.pos = pos;
            
        spawnedPlatform = Instantiate(platform, pos, Quaternion.identity).GetComponent<InteractableObstacle>();

        if (currentRule.objectType == ObjectType.HOOK)
        {
            Launcher previousLauncher = previousPlatform.GetComponent<Launcher>();
            previousLauncher.ChangeProceedingHook(spawnedPlatform);
        }

        previousPlatform = spawnedPlatform;


    }

    private void ChangeRule(Rule rule)
    {
        previousRule = currentRule;
        currentRule = rule;
    }
}