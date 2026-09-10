'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-03-2014
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
Imports DevExpress.Xpo
Imports Presentation.Common.MVP
#End Region

''' <summary>
''' Presentador del frontal Cuentas de Terceros
''' </summary>
Public Class PThirdPartyAccount

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IThirdPartyAccount

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IThirdPartyAccount)
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
    ''' Carga todos los Bancos
    ''' </summary>
    Public Sub InitializeBank()
        Using modelXPO As New MBusqueda
            Me.View.BankDatasource = modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Bank)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the third.
    ''' </summary>
    Public Sub InitializeThird()
        Using modelXPO As New MBusqueda
            Me.View.ThirdPartyDatasource = modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the city.
    ''' </summary>
    Public Sub InitializeCity()
        Using modelXPO As New MBusqueda
            Me.View.CityDatasource = modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.AllCity)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the city bank.
    ''' </summary>
    Public Sub InitializeCityBank()
        Using modelXPO As New MBusqueda
            Me.View.BankCityDatasource = modelXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.AllCity)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene la cabecera de la secuencia y la envia al frontal
    ''' </summary>
    Public Async Sub GetSequence()
        Using ModelCommonTreasury As New MCommonTreasury(Me.View.MyTag)
            Me.View.Sequence = Await ModelCommonTreasury.GetSequense()
        End Using
    End Sub

End Class
