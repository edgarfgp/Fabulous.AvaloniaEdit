namespace Fabulous.Avalonia

open AvaloniaEdit
open AvaloniaEdit.Document
open Fabulous
open Fabulous.Avalonia
open Fabulous.StackAllocatedCollections.StackList

type IFabTextEditor =
    inherit IFabTemplatedControl

module TextEditor =
    let WidgetKey = Widgets.register<TextEditor> ()

    let Document =
        Attributes.defineAvaloniaPropertyWithEquality TextEditor.DocumentProperty

    let Options =
        Attributes.defineAvaloniaPropertyWithEquality TextEditor.OptionsProperty

    let DocumentChanged =
        Attributes.defineEvent "TextEditor_DocumentChanged" (fun target -> (target :?> TextEditor).DocumentChanged)

[<AutoOpen>]
module TextEditorBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a TextEditor widget.</summary>
        static member TextEditor(fn: DocumentChangedEventArgs -> 'msg) =
            WidgetBuilder<'msg, IFabTextEditor>(
                TextEditor.WidgetKey,
                AttributesBundle(StackList.one (TextEditor.DocumentChanged.WithValue(fn)), ValueNone, ValueNone)
            )
