using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using InternalAssets.Config.LevelConfigs;
using SceneManagment;

public class EndLevelSystem
{
    private readonly EndLevelWindow _endLevelWindow;
    private readonly SaveSystem _saveSystem;
    private readonly GameEnterParams _gameEnterParams;
    private readonly LevelsConfig _levelsConfig;
    private readonly Timer _timer;


    public EndLevelSystem(EndLevelWindow endLevelWindow, SaveSystem saveSystem,
        GameEnterParams gameEnterParams, LevelsConfig levelsConfig, Timer timer)
    {
        _endLevelWindow = endLevelWindow;
        _levelsConfig = levelsConfig;
        _saveSystem = saveSystem;
        _gameEnterParams = gameEnterParams;
        _timer = timer;
    }

    public void LevelPassed(bool isPassed)
    {
        if (isPassed)
        {
            TrySaveProgress();
            _endLevelWindow.ShowWinLevelWindow();
        }
        else
        {
            _endLevelWindow.ShowLoseLevelWindow();
        }

        _timer.Stop();
    }

    private void TrySaveProgress()
    {
        var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
        var wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
        var reward = _levelsConfig.GetReward();
        if (_gameEnterParams.Location != progress.CurrentLocation ||
            _gameEnterParams.Level != progress.CurrentLevel) return;
        var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
        if (progress.CurrentLocation > maxLocationAndLevel.x ||
            (progress.CurrentLocation == maxLocationAndLevel.x &&
             progress.CurrentLevel > maxLocationAndLevel.y)) return;
        var maxLevel = _levelsConfig.GetMaxLevelOnLocation(progress.CurrentLocation);
        if (progress.CurrentLevel + 1 > maxLevel)
        {
            progress.CurrentLevel = 1;
            progress.CurrentLocation++;

        }
        else
        {
            progress.CurrentLevel++;
        }
        wallet.Coins += reward;
        _saveSystem.SaveData(SavableObjectType.Wallet);
        _saveSystem.SaveData(SavableObjectType.Progress);
    }
}