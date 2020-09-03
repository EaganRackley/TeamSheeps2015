using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerDemoScene : MonoBehaviour
{
    [System.Serializable]
    public struct Waypoint
    {
        [SerializeField]
        public float TimeOffset;
        [SerializeField]
        public Vector3 PositionToFollow;
        public Waypoint(float time, Vector3 pos)
        {
            TimeOffset = time;
            PositionToFollow = pos;
        }
    }

    public PlayerController Player1;
    public PlayerController Player2;
    public Transform P1TransformToFollow;
    public Transform P2TransformToFollow;
    private List<Waypoint> P1Waypoints = new List<Waypoint>();
    private List<Waypoint> P2Waypoints = new List<Waypoint>();
    private bool m_triggered = false;
    private int m_p1WaypointIndex = 0;
    private int m_p2WaypointIndex = 0;
    private EventManager m_eventManager;

    // Start is called before the first frame update
    void Start()
    {
        m_triggered = false;
        m_p1WaypointIndex = 0;
        m_p2WaypointIndex = 0;
        m_eventManager = FindObjectOfType<EventManager>();
        SetupP1Waypoints();
        SetupP2Waypoints();
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    void OnDrawGizmos()
    {
        DrawWaypointGizmos(Player1, P1Waypoints, Color.blue);
        DrawWaypointGizmos(Player2, P2Waypoints, Color.yellow);
    }

    void DrawWaypointGizmos(PlayerController player, List<Waypoint> waypoints, Color color)
    {
        if (player != null)
        {
            // Draws a blue line for each waypoint that Player1 will follow
            Vector3 lastPosition = player.transform.position;
            for (int index = 0; index < waypoints.Count; index++)
            {
                if (waypoints[index].PositionToFollow != null)
                {
                    Gizmos.DrawLine(lastPosition, waypoints[index].PositionToFollow);
                    lastPosition = waypoints[index].PositionToFollow;
                }
            }
        }
    }

    private int UpdatePlayerWaypoints(PlayerController player, ref Transform transformToFollow, List<Waypoint> Waypoints, int index)
    {
        if (index >= 0 && index < Waypoints.Count)
        {
            //    Waypoint point = Waypoints[index];
            //    if (m_eventManager.lifeSpent >= point.TimeOffset)
            //    {
            Waypoint point = Waypoints[index];
            transformToFollow.position = point.PositionToFollow;
            index++;
            //    }
        }
        return index;
    }


    void UpdatePositions()
    {
        m_p1WaypointIndex = UpdatePlayerWaypoints(Player1, ref P1TransformToFollow, P1Waypoints, m_p1WaypointIndex);
        m_p2WaypointIndex = UpdatePlayerWaypoints(Player2, ref P2TransformToFollow, P2Waypoints, m_p2WaypointIndex);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_triggered && m_eventManager.lifeSpent >= 7.0f)
        {
            m_triggered = true;
            P1TransformToFollow.position = Player1.transform.position;
            P2TransformToFollow.position = Player2.transform.position;
            Player1.TransformToFollow = P1TransformToFollow;
            Player2.TransformToFollow = P2TransformToFollow;
            Player1.Following = true;
            Player2.Following = true;
            InvokeRepeating("UpdatePositions", 1f, 1f);  //1s delay, repeat every 1s, debug output that we can then add as demo movement.
        }

        //if (m_triggered)
        //{
        //    m_p1WaypointIndex = UpdatePlayerWaypoints(Player1, ref P1TransformToFollow, P1Waypoints, m_p1WaypointIndex);
        //    m_p2WaypointIndex = UpdatePlayerWaypoints(Player2, ref P2TransformToFollow, P2Waypoints, m_p2WaypointIndex);
        //}

    }

    void SetupP1Waypoints()
    {
        P1Waypoints.Add(new Waypoint(7.024097f, new Vector3(0.2f, 17.9f, -0.84f)));
        P1Waypoints.Add(new Waypoint(8.026184f, new Vector3(0.2f, 17.9f, -0.84f)));
        P1Waypoints.Add(new Waypoint(9.035927f, new Vector3(0.2f, 17.08232f, -0.84f)));
        P1Waypoints.Add(new Waypoint(10.01936f, new Vector3(0.2f, 17.08232f, -0.84f)));
        P1Waypoints.Add(new Waypoint(11.02209f, new Vector3(0.2f, 17.08232f, -0.84f)));
        P1Waypoints.Add(new Waypoint(12.01971f, new Vector3(0.2f, 16.05167f, -0.84f)));
        P1Waypoints.Add(new Waypoint(13.02729f, new Vector3(0.2f, 14.79046f, -0.84f)));
        P1Waypoints.Add(new Waypoint(14.0277f, new Vector3(0.2f, 14.79046f, -0.84f)));
        P1Waypoints.Add(new Waypoint(15.03522f, new Vector3(0.2f, 14.79046f, -0.84f)));
        P1Waypoints.Add(new Waypoint(16.02465f, new Vector3(0.2f, 13.59466f, -0.8428265f)));
        P1Waypoints.Add(new Waypoint(17.03575f, new Vector3(0.2898517f, 11.87827f, -0.8401967f)));
        P1Waypoints.Add(new Waypoint(18.0279f, new Vector3(0.442223f, 10.07864f, -0.84f)));
        P1Waypoints.Add(new Waypoint(19.02102f, new Vector3(0.4408638f, 9.131805f, -0.84f)));
        P1Waypoints.Add(new Waypoint(20.02544f, new Vector3(0.44f, 9.097953f, -0.8396635f)));
        P1Waypoints.Add(new Waypoint(21.02582f, new Vector3(0.4400182f, 9.042882f, -0.84f)));
        P1Waypoints.Add(new Waypoint(22.02767f, new Vector3(0.4426536f, 7.849354f, -0.84f)));
        P1Waypoints.Add(new Waypoint(23.03069f, new Vector3(0.4426579f, 5.895368f, -0.84f)));
        P1Waypoints.Add(new Waypoint(24.02535f, new Vector3(0.4425969f, 3.942231f, -0.84f)));
        P1Waypoints.Add(new Waypoint(25.01215f, new Vector3(0.4425989f, 2.986632f, -0.84f)));
        P1Waypoints.Add(new Waypoint(26.02613f, new Vector3(0.4425989f, 2.986632f, -0.84f)));
        P1Waypoints.Add(new Waypoint(27.02145f, new Vector3(0.4421931f, 2.985878f, -0.84f)));
        P1Waypoints.Add(new Waypoint(28.03337f, new Vector3(0.4422227f, 1.906174f, -0.84f)));
        P1Waypoints.Add(new Waypoint(29.02545f, new Vector3(0.4420937f, -0.04233875f, -0.84f)));
        P1Waypoints.Add(new Waypoint(30.02923f, new Vector3(0.4420637f, -1.994747f, -0.84f)));
        P1Waypoints.Add(new Waypoint(31.024f, new Vector3(0.4419523f, -2.397331f, -0.8399232f)));
        P1Waypoints.Add(new Waypoint(32.02615f, new Vector3(0.4419641f, -2.710122f, -0.84f)));
        P1Waypoints.Add(new Waypoint(33.03315f, new Vector3(0.4419641f, -2.710122f, -0.84f)));
        P1Waypoints.Add(new Waypoint(34.02992f, new Vector3(0.8496783f, -3.125257f, -0.84f)));
        P1Waypoints.Add(new Waypoint(35.02806f, new Vector3(2.071102f, -4.534578f, -0.8401281f)));
        P1Waypoints.Add(new Waypoint(36.02786f, new Vector3(2.736449f, -5.043763f, -0.84f)));
        P1Waypoints.Add(new Waypoint(37.0224f, new Vector3(3.776996f, -5.043767f, -0.84f)));
        P1Waypoints.Add(new Waypoint(38.00954f, new Vector3(5.763875f, -5.043748f, -0.84f)));
        P1Waypoints.Add(new Waypoint(39.03192f, new Vector3(6.796227f, -5.043753f, -0.84f)));
        P1Waypoints.Add(new Waypoint(40.02659f, new Vector3(6.796227f, -5.043753f, -0.84f)));
        P1Waypoints.Add(new Waypoint(41.03576f, new Vector3(6.796227f, -5.043753f, -0.84f)));
        P1Waypoints.Add(new Waypoint(42.0226f, new Vector3(7.363359f, -5.043729f, -0.8400002f)));
        P1Waypoints.Add(new Waypoint(43.03905f, new Vector3(9.319119f, -5.043732f, -0.8400008f)));
        P1Waypoints.Add(new Waypoint(44.0263f, new Vector3(11.27201f, -5.043738f, -0.8400044f)));
        P1Waypoints.Add(new Waypoint(45.02318f, new Vector3(13.01738f, -5.053698f, -0.8400001f)));
        P1Waypoints.Add(new Waypoint(46.03475f, new Vector3(14.37616f, -5.059999f, -0.8399532f)));
        P1Waypoints.Add(new Waypoint(47.02327f, new Vector3(15.57346f, -5.054271f, -0.84f)));
        P1Waypoints.Add(new Waypoint(48.03835f, new Vector3(15.66242f, -5.056767f, -0.84f)));
        P1Waypoints.Add(new Waypoint(49.02339f, new Vector3(15.65989f, -4.721224f, -0.84f)));
        P1Waypoints.Add(new Waypoint(50.02943f, new Vector3(15.65692f, -4.70399f, -0.84f)));
        P1Waypoints.Add(new Waypoint(51.03341f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(52.02475f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(53.02578f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(54.03486f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(55.02346f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(56.02701f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(57.03412f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(58.02875f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(59.03896f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(60.03153f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(61.03799f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(62.01948f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(63.03909f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(64.0392f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(65.02843f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(66.02305f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(67.03452f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(68.03072f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(69.02853f, new Vector3(15.65692f, -5.055868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(70.03474f, new Vector3(15.65693f, -6.054237f, -0.84f)));
        P1Waypoints.Add(new Waypoint(71.02574f, new Vector3(15.65692f, -7.008718f, -0.84f)));
        P1Waypoints.Add(new Waypoint(72.03018f, new Vector3(15.65692f, -7.008718f, -0.84f)));
        P1Waypoints.Add(new Waypoint(73.03024f, new Vector3(17.12507f, -7.008681f, -0.8424002f)));
        P1Waypoints.Add(new Waypoint(74.02161f, new Vector3(18.82625f, -7.134091f, -0.84f)));
        P1Waypoints.Add(new Waypoint(75.02365f, new Vector3(19.17596f, -7.196462f, -0.84f)));
        P1Waypoints.Add(new Waypoint(76.02886f, new Vector3(19.17598f, -8.971723f, -0.84f)));
        P1Waypoints.Add(new Waypoint(77.02699f, new Vector3(19.17612f, -10.9173f, -0.84f)));
        P1Waypoints.Add(new Waypoint(78.03089f, new Vector3(19.17612f, -10.93493f, -0.84f)));
        P1Waypoints.Add(new Waypoint(79.0209f, new Vector3(19.1703f, -10.94653f, -0.84f)));
        P1Waypoints.Add(new Waypoint(80.03219f, new Vector3(19.11772f, -10.98008f, -0.84f)));
        P1Waypoints.Add(new Waypoint(81.0221f, new Vector3(19.11772f, -10.97988f, -0.84f)));
        P1Waypoints.Add(new Waypoint(82.03326f, new Vector3(19.11772f, -10.97966f, -0.84f)));
        P1Waypoints.Add(new Waypoint(83.0349f, new Vector3(19.11769f, -11.58578f, -0.8400035f)));
        P1Waypoints.Add(new Waypoint(84.03925f, new Vector3(19.11744f, -13.53562f, -0.8400213f)));
        P1Waypoints.Add(new Waypoint(85.02929f, new Vector3(19.21906f, -13.75103f, -0.84f)));
        P1Waypoints.Add(new Waypoint(86.03684f, new Vector3(20.63683f, -13.91443f, -0.84f)));
        P1Waypoints.Add(new Waypoint(87.03067f, new Vector3(21.23125f, -14.03498f, -0.84f)));
        P1Waypoints.Add(new Waypoint(88.02031f, new Vector3(21.13764f, -14.12811f, -0.84f)));
        P1Waypoints.Add(new Waypoint(89.0369f, new Vector3(21.13442f, -14.12868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(90.03737f, new Vector3(21.13442f, -14.12868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(91.01418f, new Vector3(21.13442f, -14.12868f, -0.84f)));
        P1Waypoints.Add(new Waypoint(92.01883f, new Vector3(22.13368f, -14.12852f, -0.8417665f)));
        P1Waypoints.Add(new Waypoint(93.02087f, new Vector3(22.77593f, -14.9103f, -0.84f)));
        P1Waypoints.Add(new Waypoint(94.02177f, new Vector3(22.77593f, -14.91081f, -0.84f)));
        P1Waypoints.Add(new Waypoint(95.03326f, new Vector3(23.67542f, -14.9108f, -0.84f)));
        P1Waypoints.Add(new Waypoint(96.02718f, new Vector3(23.87451f, -14.91081f, -0.84f)));
        P1Waypoints.Add(new Waypoint(97.02449f, new Vector3(25.41959f, -14.91087f, -0.84f)));
        P1Waypoints.Add(new Waypoint(98.01957f, new Vector3(27.39885f, -14.91074f, -0.84f)));
        P1Waypoints.Add(new Waypoint(99.03636f, new Vector3(27.76617f, -14.91074f, -0.84f)));
        P1Waypoints.Add(new Waypoint(100.0249f, new Vector3(27.76617f, -14.91074f, -0.84f)));
        P1Waypoints.Add(new Waypoint(101.0318f, new Vector3(27.76617f, -14.91074f, -0.84f)));
        P1Waypoints.Add(new Waypoint(102.0366f, new Vector3(29.26929f, -14.91082f, -0.8400044f)));
        P1Waypoints.Add(new Waypoint(103.0322f, new Vector3(31.22383f, -14.91081f, -0.840036f)));
        P1Waypoints.Add(new Waypoint(104.0207f, new Vector3(31.47449f, -14.91082f, -0.84f)));
        P1Waypoints.Add(new Waypoint(105.0382f, new Vector3(31.47449f, -14.91082f, -0.84f)));
        P1Waypoints.Add(new Waypoint(106.0314f, new Vector3(31.47449f, -14.91082f, -0.84f)));
        P1Waypoints.Add(new Waypoint(107.0303f, new Vector3(33.75174f, -14.91084f, -0.84f)));
        P1Waypoints.Add(new Waypoint(108.0237f, new Vector3(35.78832f, -14.91082f, -0.84f)));
        P1Waypoints.Add(new Waypoint(109.0219f, new Vector3(35.78832f, -14.91082f, -0.84f)));
        P1Waypoints.Add(new Waypoint(110.0396f, new Vector3(35.78832f, -14.91082f, -0.84f)));
        P1Waypoints.Add(new Waypoint(111.0335f, new Vector3(35.42716f, -13.4008f, -0.84f)));
        P1Waypoints.Add(new Waypoint(112.0265f, new Vector3(35.41941f, -14.17071f, -0.84f)));
        P1Waypoints.Add(new Waypoint(113.0327f, new Vector3(34.99511f, -14.2068f, -0.8400002f)));
        P1Waypoints.Add(new Waypoint(114.0219f, new Vector3(34.13379f, -14.14376f, -0.84f)));
        P1Waypoints.Add(new Waypoint(115.0393f, new Vector3(33.96432f, -13.616f, -0.84f)));
        P1Waypoints.Add(new Waypoint(116.0211f, new Vector3(32.56039f, -13.54627f, -0.84f)));
        P1Waypoints.Add(new Waypoint(117.0325f, new Vector3(31.4628f, -13.87958f, -0.84f)));
        P1Waypoints.Add(new Waypoint(118.0289f, new Vector3(30.3189f, -13.832f, -0.84f)));
        P1Waypoints.Add(new Waypoint(119.0281f, new Vector3(29.8727f, -13.90557f, -0.84f)));
        P1Waypoints.Add(new Waypoint(120.0243f, new Vector3(29.8727f, -13.79489f, -0.84f)));
        P1Waypoints.Add(new Waypoint(121.0316f, new Vector3(29.79097f, -13.76243f, -0.84f)));
        P1Waypoints.Add(new Waypoint(122.0267f, new Vector3(29.56926f, -13.7828f, -0.84f)));
        P1Waypoints.Add(new Waypoint(123.0392f, new Vector3(29.0033f, -13.87159f, -0.84f)));
        P1Waypoints.Add(new Waypoint(124.029f, new Vector3(27.94646f, -13.91501f, -0.8400001f)));
        P1Waypoints.Add(new Waypoint(125.0389f, new Vector3(26.8299f, -13.80704f, -0.84f)));
        P1Waypoints.Add(new Waypoint(126.0235f, new Vector3(25.70709f, -13.99564f, -0.84f)));
        P1Waypoints.Add(new Waypoint(127.0245f, new Vector3(25.71437f, -14.00548f, -0.84f)));
        P1Waypoints.Add(new Waypoint(128.0309f, new Vector3(25.6628f, -14.01841f, -0.84f)));
        P1Waypoints.Add(new Waypoint(129.0223f, new Vector3(24.53037f, -14.19483f, -0.84f)));
        P1Waypoints.Add(new Waypoint(130.0146f, new Vector3(24.58557f, -13.60144f, -0.84f)));
        P1Waypoints.Add(new Waypoint(131.0387f, new Vector3(24.52926f, -13.7452f, -0.84f)));
        P1Waypoints.Add(new Waypoint(132.0252f, new Vector3(23.47158f, -13.75302f, -0.84f)));
        P1Waypoints.Add(new Waypoint(133.0234f, new Vector3(23.47431f, -13.30983f, -0.8400947f)));
        P1Waypoints.Add(new Waypoint(134.0292f, new Vector3(23.47433f, -12.59051f, -0.84f)));
        P1Waypoints.Add(new Waypoint(135.0206f, new Vector3(23.48026f, -13.83453f, -0.84f)));
        P1Waypoints.Add(new Waypoint(136.0192f, new Vector3(22.83579f, -14.00955f, -0.84f)));
        P1Waypoints.Add(new Waypoint(137.0264f, new Vector3(22.10654f, -14.13729f, -0.8399829f)));
        P1Waypoints.Add(new Waypoint(138.0358f, new Vector3(22.13289f, -13.2414f, -0.8373621f)));
        P1Waypoints.Add(new Waypoint(139.0318f, new Vector3(21.05637f, -13.24148f, -0.8400002f)));
        P1Waypoints.Add(new Waypoint(140.0273f, new Vector3(21.72028f, -13.24148f, -0.84f)));
        P1Waypoints.Add(new Waypoint(141.0261f, new Vector3(21.24976f, -12.93481f, -0.84f)));
        P1Waypoints.Add(new Waypoint(142.0293f, new Vector3(21.24959f, -13.56424f, -0.84f)));
        P1Waypoints.Add(new Waypoint(143.0232f, new Vector3(21.52232f, -13.60113f, -0.84f)));
        P1Waypoints.Add(new Waypoint(144.016f, new Vector3(21.52232f, -13.68271f, -0.84f)));
        P1Waypoints.Add(new Waypoint(145.0143f, new Vector3(21.52232f, -13.68271f, -0.84f)));
        P1Waypoints.Add(new Waypoint(146.0333f, new Vector3(20.53023f, -13.68276f, -0.84f)));
        P1Waypoints.Add(new Waypoint(147.0282f, new Vector3(19.50287f, -13.68265f, -0.84f)));
        P1Waypoints.Add(new Waypoint(148.0262f, new Vector3(19.50297f, -13.42392f, -0.8417448f)));
        P1Waypoints.Add(new Waypoint(149.019f, new Vector3(19.50303f, -11.65368f, -0.84f)));
        P1Waypoints.Add(new Waypoint(150.0304f, new Vector3(19.50303f, -11.57774f, -0.84f)));
        P1Waypoints.Add(new Waypoint(151.0257f, new Vector3(19.50301f, -9.714427f, -0.84f)));
        P1Waypoints.Add(new Waypoint(152.0275f, new Vector3(19.50302f, -9.364482f, -0.8404092f)));
        P1Waypoints.Add(new Waypoint(153.0278f, new Vector3(19.50371f, -7.425918f, -0.7450564f)));
        P1Waypoints.Add(new Waypoint(154.0193f, new Vector3(18.65779f, -6.909788f, -0.84f)));
        P1Waypoints.Add(new Waypoint(155.0307f, new Vector3(17.74424f, -6.909115f, -0.84f)));
        P1Waypoints.Add(new Waypoint(156.0093f, new Vector3(17.74289f, -5.263226f, -0.8400024f)));
        P1Waypoints.Add(new Waypoint(157.0341f, new Vector3(17.7419f, -3.388163f, -0.7942456f)));
        P1Waypoints.Add(new Waypoint(158.0212f, new Vector3(16.8194f, -2.966701f, -0.8408079f)));
        P1Waypoints.Add(new Waypoint(159.0317f, new Vector3(15.53337f, -2.667969f, -0.84f)));
        P1Waypoints.Add(new Waypoint(160.0199f, new Vector3(15.7366f, -3.72949f, -0.84f)));
        P1Waypoints.Add(new Waypoint(161.0342f, new Vector3(14.74255f, -3.524787f, -0.84f)));
        P1Waypoints.Add(new Waypoint(162.0261f, new Vector3(12.88463f, -3.523396f, -0.84f)));
        P1Waypoints.Add(new Waypoint(163.0281f, new Vector3(12.00144f, -3.52332f, -0.8401033f)));
        P1Waypoints.Add(new Waypoint(164.0315f, new Vector3(10.06238f, -3.522824f, -0.8416089f)));
        P1Waypoints.Add(new Waypoint(165.0288f, new Vector3(9.163358f, -3.713899f, -0.84f)));
        P1Waypoints.Add(new Waypoint(166.033f, new Vector3(7.469412f, -3.715141f, -0.84f)));
        P1Waypoints.Add(new Waypoint(167.0063f, new Vector3(6.952423f, -3.71438f, -0.8400121f)));
        P1Waypoints.Add(new Waypoint(168.0215f, new Vector3(5.842255f, -3.715044f, -0.84f)));
        P1Waypoints.Add(new Waypoint(169.0355f, new Vector3(5.842255f, -3.715044f, -0.84f)));
        P1Waypoints.Add(new Waypoint(170.0241f, new Vector3(5.842255f, -3.715044f, -0.84f)));
        P1Waypoints.Add(new Waypoint(171.0303f, new Vector3(4.188221f, -3.715074f, -0.84f)));
        P1Waypoints.Add(new Waypoint(172.0231f, new Vector3(3.433618f, -3.715449f, -0.84f)));
        P1Waypoints.Add(new Waypoint(173.0222f, new Vector3(2.281631f, -3.715816f, -0.84f)));
        P1Waypoints.Add(new Waypoint(174.0332f, new Vector3(0.5660818f, -3.715781f, -0.84f)));
        P1Waypoints.Add(new Waypoint(175.0381f, new Vector3(0.5642492f, -2.949293f, -0.84f)));
        P1Waypoints.Add(new Waypoint(176.0272f, new Vector3(0.5644379f, -1.0151f, -0.84f)));
        P1Waypoints.Add(new Waypoint(177.0322f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(178.0236f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(179.0284f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(180.0328f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(181.0218f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(182.0212f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(183.0114f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(184.019f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(185.0186f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(186.0395f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(187.0215f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(188.0159f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(189.0324f, new Vector3(0.5644358f, -0.9154707f, -0.84f)));
        P1Waypoints.Add(new Waypoint(190.0225f, new Vector3(0.5644358f, -0.9154707f, -0.6254971f)));
        P1Waypoints.Add(new Waypoint(191.0184f, new Vector3(0.5644358f, -0.9154707f, 0.8392288f)));
        P1Waypoints.Add(new Waypoint(192.0337f, new Vector3(0.5644358f, -0.9154707f, 3.077538f)));

    }

    void SetupP2Waypoints()
    {
        P2Waypoints.Add(new Waypoint(7.024097f, new Vector3(0.9739079f, 17.04004f, -0.84f)));
        P2Waypoints.Add(new Waypoint(8.026184f, new Vector3(0.9739079f, 17.04004f, -0.84f)));
        P2Waypoints.Add(new Waypoint(9.035927f, new Vector3(0.9739079f, 17.04004f, -0.84f)));
        P2Waypoints.Add(new Waypoint(10.01936f, new Vector3(0.9739079f, 17.04004f, -0.84f)));
        P2Waypoints.Add(new Waypoint(11.02209f, new Vector3(0.9739079f, 17.04004f, -0.84f)));
        P2Waypoints.Add(new Waypoint(12.01971f, new Vector3(0.9739084f, 16.32476f, -0.84f)));
        P2Waypoints.Add(new Waypoint(13.02729f, new Vector3(0.973909f, 15.45176f, -0.84f)));
        P2Waypoints.Add(new Waypoint(14.0277f, new Vector3(0.973909f, 15.45176f, -0.84f)));
        P2Waypoints.Add(new Waypoint(15.03522f, new Vector3(0.973909f, 15.45176f, -0.84f)));
        P2Waypoints.Add(new Waypoint(16.02465f, new Vector3(0.973909f, 14.64237f, -0.84f)));
        P2Waypoints.Add(new Waypoint(17.03575f, new Vector3(0.9739549f, 13.33435f, -0.84f)));
        P2Waypoints.Add(new Waypoint(18.0279f, new Vector3(0.9739553f, 12.03032f, -0.84f)));
        P2Waypoints.Add(new Waypoint(19.02102f, new Vector3(0.9739556f, 10.73858f, -0.84f)));
        P2Waypoints.Add(new Waypoint(20.02544f, new Vector3(1.018677f, 9.493005f, -0.8400013f)));
        P2Waypoints.Add(new Waypoint(21.02582f, new Vector3(1.139847f, 9.081525f, -0.84f)));
        P2Waypoints.Add(new Waypoint(22.02767f, new Vector3(1.139841f, 8.309658f, -0.84f)));
        P2Waypoints.Add(new Waypoint(23.03069f, new Vector3(1.140074f, 7.05359f, -0.84f)));
        P2Waypoints.Add(new Waypoint(24.02535f, new Vector3(1.140067f, 5.807135f, -0.84f)));
        P2Waypoints.Add(new Waypoint(25.01215f, new Vector3(1.140709f, 4.548617f, -0.8401272f)));
        P2Waypoints.Add(new Waypoint(26.02613f, new Vector3(1.140264f, 3.349226f, -0.84f)));
        P2Waypoints.Add(new Waypoint(27.02145f, new Vector3(1.142067f, 3.021313f, -0.84f)));
        P2Waypoints.Add(new Waypoint(28.03337f, new Vector3(1.142069f, 2.355896f, -0.84f)));
        P2Waypoints.Add(new Waypoint(29.02545f, new Vector3(1.142585f, 1.160058f, -0.84f)));
        P2Waypoints.Add(new Waypoint(30.02923f, new Vector3(1.142578f, -0.0290904f, -0.84f)));
        P2Waypoints.Add(new Waypoint(31.024f, new Vector3(1.14258f, -1.208756f, -0.84f)));
        P2Waypoints.Add(new Waypoint(32.02615f, new Vector3(1.143259f, -2.377421f, -0.8401021f)));
        P2Waypoints.Add(new Waypoint(33.03315f, new Vector3(1.142769f, -2.806688f, -0.84f)));
        P2Waypoints.Add(new Waypoint(34.02992f, new Vector3(1.548294f, -3.095232f, -0.84f)));
        P2Waypoints.Add(new Waypoint(35.02806f, new Vector3(2.41629f, -3.889879f, -0.84f)));
        P2Waypoints.Add(new Waypoint(36.02786f, new Vector3(2.71196f, -4.276466f, -0.84f)));
        P2Waypoints.Add(new Waypoint(37.0224f, new Vector3(3.308143f, -4.276467f, -0.84f)));
        P2Waypoints.Add(new Waypoint(38.00954f, new Vector3(4.439787f, -4.276391f, -0.84f)));
        P2Waypoints.Add(new Waypoint(39.03192f, new Vector3(5.521914f, -4.276391f, -0.84f)));
        P2Waypoints.Add(new Waypoint(40.02659f, new Vector3(6.616551f, -4.276455f, -0.84f)));
        P2Waypoints.Add(new Waypoint(41.03576f, new Vector3(7.702086f, -4.276456f, -0.84f)));
        P2Waypoints.Add(new Waypoint(42.0226f, new Vector3(8.776639f, -4.276445f, -0.84f)));
        P2Waypoints.Add(new Waypoint(43.03905f, new Vector3(9.842911f, -4.276445f, -0.84f)));
        P2Waypoints.Add(new Waypoint(44.0263f, new Vector3(10.89802f, -4.276463f, -0.84f)));
        P2Waypoints.Add(new Waypoint(45.02318f, new Vector3(11.94204f, -4.276462f, -0.84f)));
        P2Waypoints.Add(new Waypoint(46.03475f, new Vector3(12.89032f, -4.279953f, -0.84f)));
        P2Waypoints.Add(new Waypoint(47.02327f, new Vector3(12.89837f, -4.312775f, -0.84f)));
        P2Waypoints.Add(new Waypoint(48.03835f, new Vector3(12.89515f, -4.314026f, -0.84f)));
        P2Waypoints.Add(new Waypoint(49.02339f, new Vector3(12.89515f, -4.314026f, -0.84f)));
        P2Waypoints.Add(new Waypoint(50.02943f, new Vector3(12.89515f, -4.314026f, -0.84f)));
        P2Waypoints.Add(new Waypoint(51.03341f, new Vector3(12.89515f, -4.314026f, -0.84f)));
        P2Waypoints.Add(new Waypoint(52.02475f, new Vector3(12.88677f, -4.065145f, -0.84f)));
        P2Waypoints.Add(new Waypoint(53.02578f, new Vector3(12.88677f, -4.06513f, -0.84f)));
        P2Waypoints.Add(new Waypoint(54.03486f, new Vector3(13.20242f, -4.079301f, -0.84f)));
        P2Waypoints.Add(new Waypoint(55.02346f, new Vector3(13.73543f, -4.057666f, -0.84f)));
        P2Waypoints.Add(new Waypoint(56.02701f, new Vector3(14.24957f, -4.042438f, -0.84f)));
        P2Waypoints.Add(new Waypoint(57.03412f, new Vector3(14.76771f, -4.022603f, -0.84f)));
        P2Waypoints.Add(new Waypoint(58.02875f, new Vector3(15.18407f, -4.022416f, -0.84f)));
        P2Waypoints.Add(new Waypoint(59.03896f, new Vector3(15.38275f, -4.017294f, -0.84f)));
        P2Waypoints.Add(new Waypoint(60.03153f, new Vector3(15.38275f, -4.017294f, -0.84f)));
        P2Waypoints.Add(new Waypoint(61.03799f, new Vector3(15.38275f, -3.507644f, -0.84f)));
        P2Waypoints.Add(new Waypoint(62.01948f, new Vector3(15.38274f, -2.91497f, -0.84f)));
        P2Waypoints.Add(new Waypoint(63.03909f, new Vector3(15.68117f, -2.91497f, -0.84f)));
        P2Waypoints.Add(new Waypoint(64.0392f, new Vector3(16.8066f, -2.914971f, -0.84f)));
        P2Waypoints.Add(new Waypoint(65.02843f, new Vector3(17.44392f, -2.914971f, -0.84f)));
        P2Waypoints.Add(new Waypoint(66.02305f, new Vector3(17.44378f, -3.874783f, -0.84f)));
        P2Waypoints.Add(new Waypoint(67.03452f, new Vector3(17.44375f, -4.969499f, -0.84f)));
        P2Waypoints.Add(new Waypoint(68.03072f, new Vector3(17.44375f, -6.058479f, -0.84f)));
        P2Waypoints.Add(new Waypoint(69.02853f, new Vector3(17.44375f, -6.781245f, -0.84f)));
        P2Waypoints.Add(new Waypoint(70.03474f, new Vector3(17.44375f, -6.781245f, -0.84f)));
        P2Waypoints.Add(new Waypoint(71.02574f, new Vector3(17.44375f, -6.781245f, -0.84f)));
        P2Waypoints.Add(new Waypoint(72.03018f, new Vector3(17.44375f, -6.781245f, -0.84f)));
        P2Waypoints.Add(new Waypoint(73.03024f, new Vector3(18.20479f, -6.781237f, -0.8400008f)));
        P2Waypoints.Add(new Waypoint(74.02161f, new Vector3(19.38212f, -6.709473f, -0.84f)));
        P2Waypoints.Add(new Waypoint(75.02365f, new Vector3(19.67996f, -6.671637f, -0.84f)));
        P2Waypoints.Add(new Waypoint(76.02886f, new Vector3(19.68005f, -7.532023f, -0.84f)));
        P2Waypoints.Add(new Waypoint(77.02699f, new Vector3(19.68017f, -8.533096f, -0.84f)));
        P2Waypoints.Add(new Waypoint(78.03089f, new Vector3(19.68018f, -9.525763f, -0.8400001f)));
        P2Waypoints.Add(new Waypoint(79.0209f, new Vector3(19.69591f, -10.48596f, -0.8400003f)));
        P2Waypoints.Add(new Waypoint(80.03219f, new Vector3(19.81913f, -11.3858f, -0.8402116f)));
        P2Waypoints.Add(new Waypoint(81.0221f, new Vector3(19.81882f, -12.34967f, -0.84f)));
        P2Waypoints.Add(new Waypoint(82.03326f, new Vector3(19.81882f, -13.30624f, -0.84f)));
        P2Waypoints.Add(new Waypoint(83.0349f, new Vector3(19.81882f, -13.65417f, -0.84f)));
        P2Waypoints.Add(new Waypoint(84.03925f, new Vector3(19.81882f, -13.65417f, -0.84f)));
        P2Waypoints.Add(new Waypoint(85.02929f, new Vector3(19.91115f, -13.65206f, -0.84f)));
        P2Waypoints.Add(new Waypoint(86.03684f, new Vector3(21.22095f, -13.52959f, -0.8400015f)));
        P2Waypoints.Add(new Waypoint(87.03067f, new Vector3(21.64202f, -13.45575f, -0.84f)));
        P2Waypoints.Add(new Waypoint(88.02031f, new Vector3(21.82593f, -13.99899f, -0.84f)));
        P2Waypoints.Add(new Waypoint(89.0369f, new Vector3(21.85075f, -14.11248f, -0.84f)));
        P2Waypoints.Add(new Waypoint(90.03737f, new Vector3(22.72438f, -14.11244f, -0.84f)));
        P2Waypoints.Add(new Waypoint(91.01418f, new Vector3(23.61125f, -14.11257f, -0.84f)));
        P2Waypoints.Add(new Waypoint(92.01883f, new Vector3(23.9626f, -14.11257f, -0.84f)));
        P2Waypoints.Add(new Waypoint(93.02087f, new Vector3(23.9626f, -14.11257f, -0.84f)));
        P2Waypoints.Add(new Waypoint(94.02177f, new Vector3(23.9626f, -14.11257f, -0.84f)));
        P2Waypoints.Add(new Waypoint(95.03326f, new Vector3(23.9626f, -14.11257f, -0.84f)));
        P2Waypoints.Add(new Waypoint(96.02718f, new Vector3(23.9626f, -14.11257f, -0.84f)));
        P2Waypoints.Add(new Waypoint(97.02449f, new Vector3(24.58868f, -14.11257f, -0.84f)));
        P2Waypoints.Add(new Waypoint(98.01957f, new Vector3(25.40319f, -14.1127f, -0.84f)));
        P2Waypoints.Add(new Waypoint(99.03636f, new Vector3(26.18113f, -14.11274f, -0.8400006f)));
        P2Waypoints.Add(new Waypoint(100.0249f, new Vector3(26.96521f, -14.1129f, -0.84f)));
        P2Waypoints.Add(new Waypoint(101.0318f, new Vector3(27.74012f, -14.11282f, -0.84f)));
        P2Waypoints.Add(new Waypoint(102.0366f, new Vector3(28.50554f, -14.11281f, -0.84f)));
        P2Waypoints.Add(new Waypoint(103.0322f, new Vector3(29.26302f, -14.1128f, -0.84f)));
        P2Waypoints.Add(new Waypoint(104.0207f, new Vector3(30.01142f, -14.11282f, -0.84f)));
        P2Waypoints.Add(new Waypoint(105.0382f, new Vector3(30.74786f, -14.11272f, -0.84f)));
        P2Waypoints.Add(new Waypoint(106.0314f, new Vector3(31.47564f, -14.11275f, -0.84f)));
        P2Waypoints.Add(new Waypoint(107.0303f, new Vector3(32.1944f, -14.11278f, -0.84f)));
        P2Waypoints.Add(new Waypoint(108.0237f, new Vector3(32.90279f, -14.11275f, -0.84f)));
        P2Waypoints.Add(new Waypoint(109.0219f, new Vector3(33.60263f, -14.11277f, -0.84f)));
        P2Waypoints.Add(new Waypoint(110.0396f, new Vector3(34.29253f, -14.11278f, -0.84f)));
        P2Waypoints.Add(new Waypoint(111.0335f, new Vector3(34.53278f, -14.11279f, -0.84f)));
        P2Waypoints.Add(new Waypoint(112.0265f, new Vector3(34.53278f, -14.11279f, -0.84f)));
        P2Waypoints.Add(new Waypoint(113.0327f, new Vector3(34.30592f, -14.08678f, -0.84f)));
        P2Waypoints.Add(new Waypoint(114.0219f, new Vector3(33.52728f, -13.78951f, -0.84f)));
        P2Waypoints.Add(new Waypoint(115.0393f, new Vector3(32.83878f, -13.68591f, -0.84f)));
        P2Waypoints.Add(new Waypoint(116.0211f, new Vector3(31.88821f, -13.73938f, -0.84f)));
        P2Waypoints.Add(new Waypoint(117.0325f, new Vector3(30.76525f, -13.82475f, -0.84f)));
        P2Waypoints.Add(new Waypoint(118.0289f, new Vector3(29.63468f, -13.68736f, -0.84f)));
        P2Waypoints.Add(new Waypoint(119.0281f, new Vector3(29.12211f, -13.62758f, -0.84f)));
        P2Waypoints.Add(new Waypoint(120.0243f, new Vector3(29.15541f, -13.62758f, -0.84f)));
        P2Waypoints.Add(new Waypoint(121.0316f, new Vector3(29.10167f, -13.61624f, -0.839901f)));
        P2Waypoints.Add(new Waypoint(122.0267f, new Vector3(28.79073f, -13.6044f, -0.84f)));
        P2Waypoints.Add(new Waypoint(123.0392f, new Vector3(28.2881f, -13.80138f, -0.84f)));
        P2Waypoints.Add(new Waypoint(124.029f, new Vector3(27.25415f, -13.8162f, -0.84f)));
        P2Waypoints.Add(new Waypoint(125.0389f, new Vector3(26.13494f, -13.72836f, -0.84f)));
        P2Waypoints.Add(new Waypoint(126.0235f, new Vector3(25.12048f, -13.57976f, -0.84f)));
        P2Waypoints.Add(new Waypoint(127.0245f, new Vector3(25.0984f, -13.67282f, -0.839935f)));
        P2Waypoints.Add(new Waypoint(128.0309f, new Vector3(24.96666f, -13.95393f, -0.84f)));
        P2Waypoints.Add(new Waypoint(129.0223f, new Vector3(23.91831f, -13.81277f, -0.84f)));
        P2Waypoints.Add(new Waypoint(130.0146f, new Vector3(23.88377f, -13.78793f, -0.84f)));
        P2Waypoints.Add(new Waypoint(131.0387f, new Vector3(23.83223f, -13.79883f, -0.84f)));
        P2Waypoints.Add(new Waypoint(132.0252f, new Vector3(22.78414f, -13.93171f, -0.84f)));
        P2Waypoints.Add(new Waypoint(133.0234f, new Vector3(22.77963f, -13.80586f, -0.84f)));
        P2Waypoints.Add(new Waypoint(134.0292f, new Vector3(22.77969f, -13.63494f, -0.84f)));
        P2Waypoints.Add(new Waypoint(135.0206f, new Vector3(22.77836f, -13.63756f, -0.84f)));
        P2Waypoints.Add(new Waypoint(136.0192f, new Vector3(22.34503f, -13.50536f, -0.84f)));
        P2Waypoints.Add(new Waypoint(137.0264f, new Vector3(22.083f, -13.43461f, -0.84f)));
        P2Waypoints.Add(new Waypoint(138.0358f, new Vector3(22.06832f, -12.52431f, -0.84f)));
        P2Waypoints.Add(new Waypoint(139.0318f, new Vector3(21.81424f, -12.52433f, -0.84f)));
        P2Waypoints.Add(new Waypoint(140.0273f, new Vector3(21.81422f, -12.52433f, -0.84f)));
        P2Waypoints.Add(new Waypoint(141.0261f, new Vector3(21.81519f, -12.52205f, -0.84f)));
        P2Waypoints.Add(new Waypoint(142.0293f, new Vector3(21.81519f, -12.52205f, -0.84f)));
        P2Waypoints.Add(new Waypoint(143.0232f, new Vector3(21.81519f, -12.52205f, -0.84f)));
        P2Waypoints.Add(new Waypoint(144.016f, new Vector3(21.81519f, -12.52205f, -0.84f)));
        P2Waypoints.Add(new Waypoint(145.0143f, new Vector3(21.81519f, -12.52205f, -0.84f)));
        P2Waypoints.Add(new Waypoint(146.0333f, new Vector3(21.81519f, -12.52205f, -0.84f)));
        P2Waypoints.Add(new Waypoint(147.0282f, new Vector3(21.81519f, -12.52205f, -0.84f)));
        P2Waypoints.Add(new Waypoint(148.0262f, new Vector3(21.81519f, -12.52205f, -0.84f)));
        P2Waypoints.Add(new Waypoint(149.019f, new Vector3(21.81519f, -12.52205f, -0.84f)));
        P2Waypoints.Add(new Waypoint(150.0304f, new Vector3(21.81519f, -12.52205f, -0.5912899f)));
        P2Waypoints.Add(new Waypoint(151.0257f, new Vector3(21.81519f, -12.52205f, 1.216445f)));
        P2Waypoints.Add(new Waypoint(152.0275f, new Vector3(21.81519f, -12.52205f, 3.280462f)));
        P2Waypoints.Add(new Waypoint(153.0278f, new Vector3(21.81519f, -12.52205f, 4.550144f)));
        P2Waypoints.Add(new Waypoint(154.0193f, new Vector3(21.81519f, -12.52205f, 5.329874f)));
        P2Waypoints.Add(new Waypoint(155.0307f, new Vector3(21.81519f, -12.52205f, 5.804428f)));
    }
}