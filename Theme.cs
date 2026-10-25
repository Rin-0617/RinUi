using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace RinUi;

/// <summary>
/// Rin の各プラグイン共通の UI テーマ（水色アクセント）。ソース共有のサブモジュールとして、
/// 各プラグインにコンパイル時に取り込んで使う。
///
/// 使い方：ウィンドウの描画の最初に <see cref="Push"/>、最後に <see cref="Pop"/>。
/// 見出しは <see cref="Heading"/>、強調ボタンは <see cref="AccentButton"/>。
/// </summary>
public static class Theme
{
    public static readonly Vector4 Good = new(0.45f, 0.9f, 0.45f, 1f);
    public static readonly Vector4 Warn = new(1f, 0.75f, 0.3f, 1f);
    public static readonly Vector4 Bad = new(1f, 0.4f, 0.4f, 1f);

    public static readonly Vector4 Accent = new(0.36f, 0.82f, 1.0f, 1f);
    public static readonly Vector4 AccentHover = new(0.52f, 0.88f, 1.0f, 1f);
    public static readonly Vector4 AccentActive = new(0.20f, 0.62f, 0.88f, 1f);

    /// <summary>アクセント色のボタンの上に乗せる文字色（濃色）。</summary>
    public static readonly Vector4 OnAccent = new(0.03f, 0.09f, 0.14f, 1f);

    /// <summary>パネル（子ウィンドウ）の背景。</summary>
    public static readonly Vector4 PanelBg = new(0.07f, 0.10f, 0.14f, 0.92f);

    /// <summary>ナビ（サイドバー）の背景。</summary>
    public static readonly Vector4 NavBg = new(0.06f, 0.09f, 0.13f, 0.95f);

    /// <summary>補足・サブタイトル用の淡い色。</summary>
    public static readonly Vector4 Subtle = new(0.55f, 0.78f, 0.9f, 1f);

    private static readonly Vector4 HeaderCol = new(0.36f, 0.82f, 1.0f, 0.35f);
    private static readonly Vector4 HeaderHoverCol = new(0.36f, 0.82f, 1.0f, 0.55f);
    private static readonly Vector4 HeaderActiveCol = new(0.36f, 0.82f, 1.0f, 0.75f);
    private static readonly Vector4 TableHeaderCol = new(0.36f, 0.82f, 1.0f, 0.20f);

    /// <summary>ウィンドウ共通のトーン（選択色・チェック・表ヘッダー・角丸）をかける。描画の最初に呼ぶ。</summary>
    public static void Push()
    {
        ImGui.PushStyleColor(ImGuiCol.Header, HeaderCol);
        ImGui.PushStyleColor(ImGuiCol.HeaderHovered, HeaderHoverCol);
        ImGui.PushStyleColor(ImGuiCol.HeaderActive, HeaderActiveCol);
        ImGui.PushStyleColor(ImGuiCol.CheckMark, Accent);
        ImGui.PushStyleColor(ImGuiCol.TableHeaderBg, TableHeaderCol);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 4f);
        ImGui.PushStyleVar(ImGuiStyleVar.GrabRounding, 4f);
        ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding, 6f);
    }

    /// <summary><see cref="Push"/> で積んだトーンを戻す。描画の最後に呼ぶ。</summary>
    public static void Pop()
    {
        ImGui.PopStyleVar(3);
        ImGui.PopStyleColor(5);
    }

    /// <summary>セクションの小見出し（アクセント色＋区切り線）。</summary>
    public static void Heading(string text)
    {
        ImGui.TextColored(Accent, text);
        ImGui.Separator();
    }

    /// <summary>中央寄せのテキスト。scale を指定すると一時的に文字を拡大する。</summary>
    public static void CenteredText(string text, Vector4 color, float scale = 1f)
    {
        if (scale != 1f)
        {
            ImGui.SetWindowFontScale(scale);
        }

        var size = ImGui.CalcTextSize(text);
        ImGui.SetCursorPosX(Math.Max(ImGui.GetCursorPosX(), (ImGui.GetWindowSize().X - size.X) * 0.5f));
        ImGui.TextColored(color, text);
        if (scale != 1f)
        {
            ImGui.SetWindowFontScale(1f);
        }
    }

    /// <summary>アクセント色（水色）のボタン。押されたら true。無効化や処理は呼び出し側で行う。</summary>
    public static bool AccentButton(string label, Vector2 size = default)
    {
        ImGui.PushStyleColor(ImGuiCol.Button, Accent);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, AccentHover);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, AccentActive);
        ImGui.PushStyleColor(ImGuiCol.Text, OnAccent);
        var clicked = ImGui.Button(label, size);
        ImGui.PopStyleColor(4);
        return clicked;
    }
}
