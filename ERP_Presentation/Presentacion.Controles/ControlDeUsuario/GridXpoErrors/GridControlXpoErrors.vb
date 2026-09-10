' Developer Express Code Central Example:
' How to show full information about a database exception that occurs in Server Mode
' 
' By default, if any database exception occurs in Server Mode, a tooltip is shown
' at the bottom left corner of a grid. This tooltip shows the first 50 characters
' of this exception. You can show a full exception text in a tooltip. For this, it
' is necessary to create a custom grid and override the
' GridView.CheckDataControllerError method. In addition, you can override the
' GridView.OnDataControllerError method to catch the moment when a database
' exception occurs. In this method, you can implement visual feedback
' corresponding to this exception. This example illustrates how to accomplish this
' task. To see an exception, group data against the Home Page column.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3660

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Registrator

Public Class GridControlXpoErrors
    Inherits GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Protected Overrides Function CreateDefaultView() As BaseView
        Return CreateView("MyGridView")
    End Function
    Protected Overrides Sub RegisterAvailableViewsCore(ByVal collection As InfoCollection)
        MyBase.RegisterAvailableViewsCore(collection)
        collection.Add(New GridViewInfoRegistrator())
    End Sub

    Friend Shadows ReadOnly Property EditorHelper() As GridEditorContainerHelper
        Get
            Return MyBase.EditorHelper
        End Get
    End Property

    Private Sub InitializeComponent()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me
        Me.GridView1.Name = "GridView1"
        '
        'MyGridControl
        '
        Me.MainView = Me.GridView1
        Me.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class
