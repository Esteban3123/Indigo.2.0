'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Giovanny Plazas
' Created          : 17/12/2023
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
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class PAddRuleRestrictionCondition

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IAddRuleRestrictionCondition

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IAddRuleRestrictionCondition)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Lista las unidades funcionales primera condicion
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListFunctionalUnit(True)
    End Function

    ''' <summary>
    ''' Lista los tipos de estancias
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllStayType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.GetAllStayType()
    End Function

    ''' <summary>
    ''' lista los tipo de quirurgicos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSurgicalGroupByStatus() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListSurgicalGroupByStatus(True)
    End Function

    ''' <summary>
    ''' lista los rangos UVR
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUVRRangeByStatus() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListUVRRangeByStatus(True)
    End Function
#End Region

End Class
