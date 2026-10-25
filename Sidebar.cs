using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface;
using Dalamud.Interface.Components;
using Dalamud.Utility;

namespace RinUi;

/// <summary>
/// 共通の左サイドバー（ナビ）。上にプラグイン名＋バージョン、中にナビ項目、下に ♥ 支援ボタン。
/// 右側のコンテンツは呼び出し側で <c>BeginChild</c> して描く（<see cref="Draw"/> は末尾で SameLine する）。
///
/// 使い方：
/// <code>
/// Theme.Push();
/// this.tab = Sidebar.Draw("PluginName", Plugin.VersionText, Labels, this.tab, SupportUrl, Tooltips);
/// if (ImGui.BeginChild("##content", Vector2.Zero, true)) { /* this.tab で切替 */ }
/// ImGui.EndChild();
/// Theme.Pop();
/// </code>
/// </summary>
public static class Sidebar
{
    private const string SupportTooltip = "OFUSEで開発を応援する";

    /// <summary>サイドバーを描き、選んだナビ項目の番号を返す。末尾で SameLine する（次に右コンテンツ）。</summary>
    public static int Draw(
        string name,
        string version,
        IReadOnlyList<string> labels,
        int selected,
        string? supportUrl = null,
        IReadOnlyList<string>? tooltips = null,
        float width = 180f)
    {
        ImGui.PushStyleColor(ImGuiCol.ChildBg, Theme.NavBg);
        if (ImGui.BeginChild("##rinui-nav", new Vector2(width, 0), true))
        {
            ImGui.SetWindowFontScale(1.15f);
            ImGui.Dummy(new Vector2(0, 2));
            ImGui.TextColored(Theme.Accent, name);
            ImGui.TextDisabled(version);
            ImGui.Dummy(new Vector2(0, 4));
            ImGui.Separator();
            ImGui.Dummy(new Vector2(0, 4));

            for (var i = 0; i < labels.Count; i++)
            {
                if (ImGui.Selectable($"  {labels[i]}", selected == i))
                {
                    selected = i;
                }

                if (tooltips is not null && i < tooltips.Count && !string.IsNullOrEmpty(tooltips[i]) && ImGui.IsItemHovered())
                {
                    ImGui.SetTooltip(tooltips[i]);
                }
            }

            if (supportUrl is { Length: > 0 })
            {
                var y = ImGui.GetWindowHeight() - ImGui.GetFrameHeightWithSpacing() - ImGui.GetStyle().WindowPadding.Y;
                ImGui.SetCursorPosY(Math.Max(ImGui.GetCursorPosY(), y));
                if (ImGuiComponents.IconButton(FontAwesomeIcon.Heart))
                {
                    Util.OpenLink(supportUrl);
                }

                if (ImGui.IsItemHovered())
                {
                    ImGui.SetTooltip(SupportTooltip);
                }
            }

            ImGui.SetWindowFontScale(1f);
        }

        ImGui.EndChild();
        ImGui.PopStyleColor();
        ImGui.SameLine();
        return selected;
    }
}
