namespace Sample
{
    public static class TextBoxExt
    {
        public static string NullIfEmpty(this TextBox textBox) =>
            string.IsNullOrWhiteSpace(textBox.Text) ? null : textBox.Text.Trim();
    }
}
