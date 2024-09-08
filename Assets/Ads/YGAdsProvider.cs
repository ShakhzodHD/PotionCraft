using YG;
public static class YGAdsProvider
{
    public static void ShowFullScreenAd()
    {
        YandexGame.FullscreenShow();
    }
    public static void ShowRewardedAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
}
