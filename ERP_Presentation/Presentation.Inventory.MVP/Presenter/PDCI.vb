'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PDCI

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IDCI

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IDCI)
        Me._sessionValues = SessionValues.Instance
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Se obtienen todos los ATCEntity
    ''' </summary>
    Public Sub InitializeATCEntity()
        View.ATCParentsXpo = XpoServiceEx.Instance(Me._sessionValues.TransactionalContainer).InventoryService.ListATCEntity()
    End Sub

    ''' <summary>
    ''' Se obtiene las unidades de medida
    ''' </summary>
    ''' <returns></returns>
    Public Function GetMeasureUnitList()
        Return XpoServiceEx.Instance(Me._sessionValues.TransactionalContainer).InventoryService.ListMeasureUnit()
    End Function

    ''' <summary>
    ''' Se obtienen todos los factores de riesgo de dbo.RiskFactor
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllRiskFactor()
        Return XpoServiceEx.Instance(Me._sessionValues.TransactionalContainer).CrystalService.ListAllRiskFactor()
    End Function

    ''' <summary>
    ''' Se obtien e un factor de riesgo por su ID.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRiskFactorById(riskFactorId As Integer)
        Return XpoServiceEx.Instance(Me._sessionValues.TransactionalContainer).CrystalService.GetRiskFactorById(riskFactorId)
    End Function
#End Region

End Class
