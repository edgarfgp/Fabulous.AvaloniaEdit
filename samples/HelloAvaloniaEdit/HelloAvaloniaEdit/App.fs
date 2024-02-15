namespace HelloAvaloniaEdit

open System
open Avalonia.Controls
open Avalonia.Markup.Xaml.Styling
open Avalonia.Media
open Avalonia.Themes.Fluent
open AvaloniaEdit.Document
open Fabulous
open Fabulous.Avalonia

open type Fabulous.Avalonia.View

module App =
    type Model = { Document: TextDocument }

    type Msg = DocumentChanged of DocumentChangedEventArgs

    let init () = { Document = TextDocument() }, Cmd.none

    let update msg model =
        match msg with
        | DocumentChanged args -> { Document = args.NewDocument }, Cmd.none

    let view model =
        VStack(16.) {
            Grid(coldefs = [ Star; Auto ], rowdefs = []) {
                Grid(coldefs = [ Star; Pixel(4); Star ], rowdefs = [ Star ]) {
                    TextEditor(DocumentChanged)

                    GridSplitter(GridResizeDirection.Columns)
                        .background(SolidColorBrush(Colors.White))
                        .gridRow(0)
                        .gridColumn (1)

                    Rectangle()
                        .height(500.)
                        .fill(SolidColorBrush(Colors.Blue))
                        .gridRow(0)
                        .gridColumn (2)
                }

                Rectangle()
                    .height(500.)
                    .width(200.)
                    .fill(SolidColorBrush(Colors.Green))
                    .gridColumn (1)
            }
        }

    let app model =
        if
            OperatingSystem.IsAndroid()
            || OperatingSystem.IsIOS()
            || OperatingSystem.IsBrowser()
        then
            SingleViewApplication(view model)
        else
            DesktopApplication(Window(view model))

    let theme () =
        StyleInclude(baseUri = null, Source = Uri("avares://HelloAvaloniaEdit/App.xaml"))

    let program = Program.statefulWithCmd init update app
