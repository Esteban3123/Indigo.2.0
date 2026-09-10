'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 20/10/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Domain.Portfolio.Model

Public Interface IPortfolioProvisionRepository
    Inherits IRepository(Of PortfolioProvision)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetPortfolioProvision(ByVal code As String) As PortfolioProvision

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioProvisionById(Id As Integer) As PortfolioProvision

    ''' <summary>
    ''' Valida el CopyPaste del form de provision y deterioro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPastePortfolioProvision(XmlObject As String, CourtDate As Date, Process As Integer, OperatingUnitId As Integer, ApplyDeterioration As Integer) As List(Of SP_CopyAndPasteProvisionAndDeterioration_Result)

    ''' <summary>
    ''' Guarda la provision y deterioro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveProvisionAndDeterioration(XmlObject As String, DetailForDeleteXml As String, CodeUser As String) As SP_SaveProvisionAndDeterioration_Result

    ''' <summary>
    ''' Confirma la provision y deterioro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ConfirmPortfolioProvision(PortfolioProvisionId As Integer, CodeUser As String, Optional operativeUnitId As Integer? = Nothing) As SP_ConfirmProvisionAndDeterioration_Result

    ''' <summary>
    ''' Obtiene y calcula la información del Deterioro de acuerdo a la clasificación
    ''' </summary>
    ''' <param name="closingDate"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="pageNumber"></param>
    ''' <param name="pageSize"></param>
    ''' <returns></returns>
    Function SP_GetPortfolioDeteriorationByClassification(closingDate As DateTime, operativeUnitId As Integer, Optional pageNumber As Integer = 1, Optional pageSize As Integer = 50000) As List(Of PortfolioDeteriorationByClassificationDTO)

    ''' <summary>
    ''' Prepara y valida la confirmación de deterioro por clasificación.
    ''' Confirma la cabecera y retorna los grupos de ThirdPartyId para procesamiento paralelo.
    ''' </summary>
    Function SP_PrepareConfirmPortfolioDeteriorationByClassification(portfolioProvisionId As Integer, codeUser As String, operativeUnitId As Integer) As (PrepareResult As PrepareConfirmDeteriorationByClassificationDTO, ThirdPartyGroups As List(Of ThirdPartyGroupDTO))

    ''' <summary>
    ''' Ejecuta la confirmación de deterioro por clasificación para cada ThirdPartyId.
    ''' </summary>
    Function SP_ConfirmPortfolioDeteriorationByClassificationBatch(portfolioProvisionId As Integer, codeUser As String, operativeUnitId As Integer, thirdPartyGroups As List(Of ThirdPartyGroupDTO)) As List(Of ConfirmDeteriorationByClassificationDTO)

    ''' <summary>
    ''' Ejecuta la confirmación completa del deterioro por clasificación de forma atómica
    ''' usando una sola SqlConnection con SqlTransaction ADO.NET, sin límite de tiempo de TransactionManager.
    ''' </summary>
    Function SP_ConfirmPortfolioDeteriorationByClassificationAtomic(portfolioProvisionId As Integer, codeUser As String, operativeUnitId As Integer) As (PrepareResult As PrepareConfirmDeteriorationByClassificationDTO, BatchResults As List(Of ConfirmDeteriorationByClassificationDTO))

End Interface
