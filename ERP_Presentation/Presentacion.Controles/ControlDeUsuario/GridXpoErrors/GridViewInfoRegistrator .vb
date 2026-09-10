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
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid

Public Class GridViewInfoRegistrator
    Inherits GridInfoRegistrator
    Public Overrides ReadOnly Property ViewName() As String
        Get
            Return "MyGridView"
        End Get
    End Property
    Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
        Return New GridViewXpoErrors(TryCast(grid, GridControl))
    End Function
End Class
