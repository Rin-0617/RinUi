# RinUi

Rin の各 Dalamud プラグイン共通の UI テーマ（水色アクセント）。

ソース共有のサブモジュールとして使います。各プラグインのリポジトリに `theme/` として追加し、
コンパイル時に `Theme.cs` を取り込みます（追加の DLL は作りません）。

## 使い方

```csharp
using RinUi;

// ウィンドウ描画の最初と最後
Theme.Push();
// ... Theme.Heading("見出し"); Theme.AccentButton("実行"); など ...
Theme.Pop();
```

## ライセンス

[AGPL-3.0](LICENSE)
