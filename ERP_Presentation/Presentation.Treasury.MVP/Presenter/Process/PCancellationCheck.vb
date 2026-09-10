'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 23-05-2014
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
Imports Presentation.Controls.MVP
#End Region

Public Class PCancellationCheck
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICancellationCheck

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICancellationCheck)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the commision account.
    ''' </summary>
    Public Sub InitializeEntityAccount()
        Using ModelXpo As New MBusqueda
            Me.View.EntityBankAccountDatasource = ModelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccount)
        End Using
    End Sub

    'Public Sub InitializeCheckBookByIdEntity()
    '    Using ModelXpo As New MBusqueda
    '        Me.View.EntityBankAccountDatasource = ModelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccount)
    '    End Using
    'End Sub

End Class
