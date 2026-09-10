Imports DevExpress.Xpo

' ***********************************************************************
' Assembly         : Presentation.Reporter
' Author           : Juan Diego Diaz
' Created          : 2014-01-04
' 
' Copyright        : (c) . All rights reserved.
' ***********************************************************************

''' <summary>
''' Interfaz que implementan los XtraReports
''' </summary>
Public Interface IReport

    ''' <summary>
    ''' Metodo donde se define la logica necesaria para cargar imagenes a un reporte
    ''' </summary>
    Sub CargarImagenes()

    'Function DataFunc(XpoObject As Object) As Boolean

    Property ParametrosReporte As Object()

    Sub CargarDataSource()

    ReadOnly Property NameReport As String

End Interface

Public Interface IReportAsync

    Function CargarDataSourceAsync() As task

End Interface

Public Interface IReferenceToReport
    Sub CargarDataSource2()
End Interface
