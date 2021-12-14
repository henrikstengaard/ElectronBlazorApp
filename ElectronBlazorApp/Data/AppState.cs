using System;

namespace ElectronBlazorApp.Data
{
    public class AppState
    {
        public string Path { get; set; }
        public string Content { get; set; }
        
        public event Action OnChange;
        public void NotifyStateChanged() => OnChange?.Invoke();

        public void SetContent(string content)
        {
            Content = content;
            NotifyStateChanged();
        }
    }
}