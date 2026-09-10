'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 02-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Domain.Payroll.Entities

#End Region
''' <summary>
''' 
''' </summary>
Public Class PResponsible

    ''' <summary>s
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IResponsible

    ''' <summary>
    ''' Variable que se usa para tratar los responsables de mantenimiento
    ''' </summary>
    Dim Responsible As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IResponsible)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Async Sub Initializes()
        View.StateResponsible = True
        'Using Model As New MCompany
        ' View.CompaniesDataSource = Await Model.ListAllHealthCenters
        ' End Using
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.View.MyTag))
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub
End Class
