======================================================================
 Undo Sample b1.1.0
 
                               GASOLINE STAND  http://hrgs.xrea.jp/
***********************************************************************


目次.
---------------------------------------
1.概要
2.動作環境
3.主なファイル構成
4.備考
5.更新履歴
---------------------------------------

1.概要
　ゲームのマップエディタのような、チップを配置してマップを作るような
　アプリでのアンドゥ・リドゥの実装のサンプルです。
　Paint.Netや、その他幾つかのアプリのソースコードを参考にしながら
　自分なりに考えてみました。このサンプルは、自分が把握しているだけでも
　いくつか課題が残っているので、サンプルながらベータ版です。
　
2.動作環境
　.Net Framework2.0を使って開発しているので、アプリを起動するには
　あらかじめインストールしておく必要があります。
　
　開発環境：
　　Visual Studio 2005 Standard Edition
　
　開発言語：
　　C#
　

3.主なファイル構成
　MenuTest/
　├Surface.cs           - マップ(絵？)データを格納するクラス。
　│                       ペイントソフトのレイヤー１枚分に相当。
　├Document.cs          - ドキュメント。マップのデータやアンドゥ情報、
　│                       ファイル名等を持つクラス
　├Canvas.cs            - サーフェースの内容を画面に表示するクラス
　├Form1.cs             - メインウィンドウ。アクティブなドキュメント、
　│                       ツール等の情報を持つクラス
　├ChildForm.cs         - ドキュメントの内容を表示するクラス。
　├IHistoryAction.cs    - アンドゥ・リドゥ可能な操作のインターフェース
　├HistoryActionList.cs - アンドゥ・リドゥ可能な操作のリスト。
　├SurfaceCopyAction.cs - サーフェース全体をアンドゥ情報として保存するクラス。
　├PenTool.cs           - ペンツール
　├LineTool.cs          - 直線ツール

4.備考
　いつになるかわかりませんが・・・次のバージョンで、
　以下の修正・機能追加を行う予定です。
　最終的にはキャラクター配置、マルチレイヤー対応とかも
　やるかもしれません。
　
　・名前空間の整頓
　・描画速度の改善
　・サイズ変更
　・ツール追加(何ツールかは未定・・・。)

5.更新履歴
　
　b1.1.0  --- コメントのスタイルをSandCastle風に変更してみる。(2006-12-03)
　b1.0.0  --- 初公開。(2006-07-27)
