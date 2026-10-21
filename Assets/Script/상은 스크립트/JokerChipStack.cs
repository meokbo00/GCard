using UnityEngine;

public class JokerChipStack : MonoBehaviour
{
    private const string REROLL_SR3_RED_CHIP_KEY = "RerollSR3RedChip";
    private const string BUYSELL_SR01_CHIP_KEY = "BuySellSR01Chip";
    private const string BUYSELL_SR1_CHIP_KEY = "BuySellSR1Chip";
    private const string HSB25_CHIP_KEY = "HSB25Chip";
    private const string TSR02_CHIP_KEY = "TSR02Chip";
    private const string SKIP_SR4_CHIP_KEY = "SkipSR4Chip";
    private const string PLAYFOUR_SB5_CHIP_KEY = "PlayFourSB5Chip";
    private const string PLAY2_SB8_CHIP_KEY = "Play2SB8Chip";

    public float RerollSR3RedChip { get; private set; }
    public float BuySellSR01Chip { get; private set; }
    public float BuySellSR1Chip { get; private set; }
    public float HSB25Chip { get; private set; }
    public float TSR02Chip { get; private set; }
    public float SkipSR4Chip { get; private set; }
    public float PlayFourSB5Chip { get; private set; }
    public float Play2SB8Chip { get; private set; }

    public enum ChipType
    {
        RerollSR3Red,
        BuySellSR01,
        BuySellSR1,
        HSB25,
        TSR02,
        SkipSR4,
        PlayFourSB5,
        Play2SB8
    }

    private static JokerChipStack _instance;
    public static JokerChipStack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<JokerChipStack>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("JokerChipStack");
                    _instance = obj.AddComponent<JokerChipStack>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAllChips();            
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddChips(ChipType chipType, float amount)
    {
        switch (chipType)
        {
            case ChipType.RerollSR3Red:
                RerollSR3RedChip += amount;
                break;
            case ChipType.BuySellSR01:
                BuySellSR01Chip += amount;
                break;
            case ChipType.BuySellSR1:
                BuySellSR1Chip += amount;
                break;
            case ChipType.HSB25:
                HSB25Chip += amount;
                break;
            case ChipType.TSR02:
                TSR02Chip += amount;
                break;
            case ChipType.SkipSR4:
                SkipSR4Chip += amount;
                break;
            case ChipType.PlayFourSB5:
                PlayFourSB5Chip += amount;
                break;
            case ChipType.Play2SB8:
                Play2SB8Chip += amount;
                break;
        }
        SaveChips(chipType);
    }

    public float GetChips(ChipType chipType)
    {
        return chipType switch
        {
            ChipType.RerollSR3Red => RerollSR3RedChip,
            ChipType.BuySellSR01 => BuySellSR01Chip,
            ChipType.BuySellSR1 => BuySellSR1Chip,
            ChipType.HSB25 => HSB25Chip,
            ChipType.TSR02 => TSR02Chip,
            ChipType.SkipSR4 => SkipSR4Chip,
            ChipType.PlayFourSB5 => PlayFourSB5Chip,
            ChipType.Play2SB8 => Play2SB8Chip,
            _ => 0f
        };
    }

    private void SaveChips(ChipType chipType)
    {
        switch (chipType)
        {
            case ChipType.RerollSR3Red:
                PlayerPrefs.SetFloat(REROLL_SR3_RED_CHIP_KEY, RerollSR3RedChip);
                break;
            case ChipType.BuySellSR01:
                PlayerPrefs.SetFloat(BUYSELL_SR01_CHIP_KEY, BuySellSR01Chip);
                break;
            case ChipType.BuySellSR1:
                PlayerPrefs.SetFloat(BUYSELL_SR1_CHIP_KEY, BuySellSR1Chip);
                break;
            case ChipType.HSB25:
                PlayerPrefs.SetFloat(HSB25_CHIP_KEY, HSB25Chip);
                break;
            case ChipType.TSR02:
                PlayerPrefs.SetFloat(TSR02_CHIP_KEY, TSR02Chip);
                break;
            case ChipType.SkipSR4:
                PlayerPrefs.SetFloat(SKIP_SR4_CHIP_KEY, SkipSR4Chip);
                break;
            case ChipType.PlayFourSB5:
                PlayerPrefs.SetFloat(PLAYFOUR_SB5_CHIP_KEY, PlayFourSB5Chip);
                break;
            case ChipType.Play2SB8:
                PlayerPrefs.SetFloat(PLAY2_SB8_CHIP_KEY, Play2SB8Chip);
                break;
        }
        PlayerPrefs.Save();
        Debug.Log($"{chipType} 저장 완료: {GetChips(chipType)}");
    }

    private void LoadAllChips()
    {
        RerollSR3RedChip = PlayerPrefs.GetFloat(REROLL_SR3_RED_CHIP_KEY, 0f);
        BuySellSR01Chip = PlayerPrefs.GetFloat(BUYSELL_SR01_CHIP_KEY, 0.0f);
        BuySellSR1Chip = PlayerPrefs.GetFloat(BUYSELL_SR1_CHIP_KEY, 0.0f);
        HSB25Chip = PlayerPrefs.GetFloat(HSB25_CHIP_KEY, 0.0f);
        TSR02Chip = PlayerPrefs.GetFloat(TSR02_CHIP_KEY, 0.0f);
        SkipSR4Chip = PlayerPrefs.GetFloat(SKIP_SR4_CHIP_KEY, 0.0f);
        PlayFourSB5Chip = PlayerPrefs.GetFloat(PLAYFOUR_SB5_CHIP_KEY, 0.0f);
        Play2SB8Chip = PlayerPrefs.GetFloat(PLAY2_SB8_CHIP_KEY, 0.0f);
        Debug.Log("모든 칩 값 불러오기 완료");
    }

    // 모든 칩 데이터 초기화 (필요시 사용)
    public void ResetAllChips()
    {
        RerollSR3RedChip = 0f;
        BuySellSR01Chip = 0.0f;
        BuySellSR1Chip = 0.0f;
        HSB25Chip = 0.0f;
        TSR02Chip = 0.0f;
        SkipSR4Chip = 0.0f;
        PlayFourSB5Chip = 0.0f;
        Play2SB8Chip = 0.0f;

        PlayerPrefs.DeleteKey(REROLL_SR3_RED_CHIP_KEY);
        PlayerPrefs.DeleteKey(BUYSELL_SR01_CHIP_KEY);
        PlayerPrefs.DeleteKey(BUYSELL_SR1_CHIP_KEY);
        PlayerPrefs.DeleteKey(HSB25_CHIP_KEY);
        PlayerPrefs.DeleteKey(TSR02_CHIP_KEY);
        PlayerPrefs.DeleteKey(SKIP_SR4_CHIP_KEY);
        PlayerPrefs.DeleteKey(PLAYFOUR_SB5_CHIP_KEY);
        PlayerPrefs.DeleteKey(PLAY2_SB8_CHIP_KEY);
        PlayerPrefs.Save();

    }
}