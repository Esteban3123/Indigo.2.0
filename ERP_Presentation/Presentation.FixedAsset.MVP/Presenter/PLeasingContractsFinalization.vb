'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/03/2017
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
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Domain.Base.Entities

#End Region

Public Class PLeasingContractsFinalization

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ILeasingContractsFinalization

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ILeasingContractsFinalization)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSettingsFixedAssetXpo(operatingUnitId As Integer) As ActionResult
        Dim filter As String = "OperatingUnitId = " & operatingUnitId
        View.SettingsFixedAssetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of SettingFixedAssetXpo)(Nothing, filter).FirstOrDefault()
        If View.SettingsFixedAssetXpo Is Nothing Then
            View.Year = 0
            View.Month = 0
            Return New ActionResult With {.StateResult = False, .Message = "No existe parámetros para la unidad operativa escogida"}
        End If
        View.Year = View.SettingsFixedAssetXpo.ProcessDate.Year
        View.Month = View.SettingsFixedAssetXpo.ProcessDate.Month
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Obtiene el listado de ingreso de activos que sean de tipo leasing y que esten dentro del mismo mes y año
    ''' </summary>
    ''' <returns></returns>
    Public Function GetListFixedAssetPhysicalAssetLeasingByMonthAndYear(Month As Integer, Year As Integer) As List(Of FixedAssetPhysicalAssetXpo)
        Dim filter As String = "AdquisitionType = " & 7 & " And NumberContractLeasing is not null And InitialDateLeasing is not null And EndDateLeasing is not null And (GetYear(EndDateLeasing) < " & Year & " Or (GetMonth(EndDateLeasing) <= " & Month & " And GetYear(EndDateLeasing) = " & Year & "))"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetPhysicalAssetXpo)(Nothing, filter).ToList()
    End Function

#End Region

End Class
