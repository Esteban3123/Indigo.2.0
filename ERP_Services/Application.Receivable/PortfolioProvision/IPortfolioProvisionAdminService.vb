'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 20/10/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Portfolio.Model

Public Interface IPortfolioProvisionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' metodo para pegar en la rejilla de plantilla de procedimientos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CopyAndPastePortfolioProvision(Data As List(Of List(Of String)), CourtDate As Date, Process As Integer, OperatingUnitId As Integer, applyDeterioration As Byte, Percentage As Decimal, Expectative As Integer) As ActionResult(Of List(Of PortfolioProvisionDetail), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetPortfolioProvision(ByVal code As String, audit As AuditMessage) As ActionResult(Of PortfolioProvision)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioProvisionById(Id As Integer, audit As AuditMessage) As ActionResult(Of PortfolioProvision)

    ''' <summary>
    ''' Guarda una provision
    ''' </summary>
    ''' <param name="PortfolioProvision"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePortfolioProvision(PortfolioProvision As PortfolioProvision, listPortfolioProvisionDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of PortfolioProvision)

    ''' <summary>
    ''' Anula una provision
    ''' </summary>
    ''' <param name="PortfolioProvision"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AnnularPortfolioProvision(PortfolioProvision As PortfolioProvision, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Confirma la provision y deterioro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmPortfolioProvision(PortfolioProvision As PortfolioProvision, audit As AuditMessage, Optional operativeUnidId As Integer? = Nothing) As ActionResult(Of PortfolioProvision)

    ''' <summary>
    ''' Confirma la provision y deterioro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAndConfirmPortfolioProvision(PortfolioProvision As PortfolioProvision, listPortfolioProvisionDetailDelete As List(Of Integer), audit As AuditMessage, Optional operativeUnitId As Integer? = Nothing) As ActionResult(Of PortfolioProvision)

    ''' <summary>
    ''' Obtiene la información de Deterioro de acuerdo a la Clasificación
    ''' </summary>
    ''' <param name="closingDate"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <returns></returns>
    Function GetPortfolioDeteriorationByClassification(closingDate As DateTime, operativeUnitId As Integer) As ActionResult(Of List(Of PortfolioDeteriorationByClassificationDTO))

End Interface
