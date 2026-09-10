'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 29-04-2013
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
Public Class PKinship
    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IKindship

    ''' <summary>
    ''' Variable que se usa para tratar el parentesco como un objeto
    ''' </summary>
    Dim Kinship As Kinship

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IKindship)
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
        View.StateKindship = True
        'Using Model As New MCompany
        ' View.CompaniesDataSource = Await Model.ListAllHealthCenters
        ' End Using
    End Sub
End Class
