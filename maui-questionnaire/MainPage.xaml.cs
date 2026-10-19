namespace maui_questionnaire;

public partial class MainPage : ContentPage
{
    private const double InitialHours = 8;
    private static readonly DateTime InitialSessionDate = new(2026, 10, 19);

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnShowResultClicked(object? sender, EventArgs e)
    {
        var name = _NameEntry_.Text?.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlertAsync("Проверьте анкету", "Введите имя.", "Хорошо");
            return;
        }

        if (!int.TryParse(_AgeEntry_.Text, out var age) || age <= 0)
        {
            await DisplayAlertAsync("Проверьте анкету", "Возраст должен быть целым положительным числом.", "Хорошо");
            return;
        }

        var platform = GetSelectedPlatform();
        var genre = _GenrePicker_.SelectedItem?.ToString() ?? "Не указано";
        var preferences = GetPreferences();
        var recommendations = _RecommendationsSwitch_.IsToggled ? "Да" : "Нет";
        var favoriteGames = string.IsNullOrWhiteSpace(_FavoriteGamesEditor_.Text)
            ? "Не указано"
            : _FavoriteGamesEditor_.Text.Trim();
        var hours = Math.Round(_HoursSlider_.Value);
        var lastSession = _LastSessionDatePicker_.Date?.ToString("dd.MM.yyyy") ?? "Не указано";

        _ResultLabel_.Text =
            $"Имя: {name}\n" +
            $"Возраст: {age}\n" +
            $"Платформа: {platform}\n" +
            $"Любимый жанр: {genre}\n" +
            $"Предпочтения: {preferences}\n" +
            $"Получать рекомендации: {recommendations}\n" +
            $"Любимые игры: {favoriteGames}\n" +
            $"Часов игры в неделю: {hours:F0}\n" +
            $"Последняя игровая сессия: {lastSession}";

        _ResultBorder_.IsVisible = true;
    }

    private void OnClearClicked(object? sender, EventArgs e)
    {
        _NameEntry_.Text = string.Empty;
        _AgeEntry_.Text = string.Empty;
        _PcRadioButton_.IsChecked = true;
        _GenrePicker_.SelectedIndex = -1;
        _StoryCheckBox_.IsChecked = false;
        _FriendsCheckBox_.IsChecked = false;
        _RecommendationsSwitch_.IsToggled = false;
        _FavoriteGamesEditor_.Text = string.Empty;
        _HoursSlider_.Value = InitialHours;
        _LastSessionDatePicker_.Date = InitialSessionDate;
        _ResultLabel_.Text = "Результат появится здесь";
        _ResultBorder_.IsVisible = false;
    }

    private string GetSelectedPlatform()
    {
        if (_ConsoleRadioButton_.IsChecked)
        {
            return _ConsoleRadioButton_.Value?.ToString() ?? "Не указано";
        }

        if (_PhoneRadioButton_.IsChecked)
        {
            return _PhoneRadioButton_.Value?.ToString() ?? "Не указано";
        }

        return _PcRadioButton_.Value?.ToString() ?? "Не указано";
    }

    private string GetPreferences()
    {
        var preferences = new List<string>();

        if (_StoryCheckBox_.IsChecked)
        {
            preferences.Add("интересный сюжет");
        }

        if (_FriendsCheckBox_.IsChecked)
        {
            preferences.Add("игра с друзьями");
        }

        return preferences.Count == 0 ? "Не указано" : string.Join(", ", preferences);
    }
}
