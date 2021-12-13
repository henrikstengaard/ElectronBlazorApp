using System;

namespace ElectronBlazorApp.Data
{
    public class TextFile
    {
        public string Path { get; set; }
        public string Content { get; set; }
        
        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void SetContent(string content)
        {
            Content = content;
            NotifyStateChanged();
        }
    }
}