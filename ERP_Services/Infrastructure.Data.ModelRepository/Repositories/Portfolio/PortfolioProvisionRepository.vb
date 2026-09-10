'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 20/10/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Portfolio.Model

Public Class PortfolioProvisionRepository
    Inherits GenericRepository(Of PortfolioProvision)
    Implements IPortfolioProvisionRepository

#Region "Context"

    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function SP_CopyAndPastePortfolioProvision(XmlObject As String, CourtDate As Date, Process As Integer, OperatingUnitId As Integer, ApplyDeterioration As Integer) As List(Of SP_CopyAndPasteProvisionAndDeterioration_Result) Implements IPortfolioProvisionRepository.SP_CopyAndPastePortfolioProvision
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteProvisionAndDeterioration(XmlObject, CourtDate, Process, OperatingUnitId, ApplyDeterioration).ToList
    End Function

    Public Function GetPortfolioProvision(code As String) As PortfolioProvision Implements IPortfolioProvisionRepository.GetPortfolioProvision
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In _context.PortfolioProvision Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d In _context.PortfolioProvision.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
            Return res
        Else
            Return New PortfolioProvision()
        End If
    End Function

    Public Function GetPortfolioProvisionById(Id As Integer) As PortfolioProvision Implements IPortfolioProvisionRepository.GetPortfolioProvisionById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d In _context.PortfolioProvision Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New PortfolioProvision()
        End If
    End Function

    Public Function SP_SaveProvisionAndDeterioration(XmlObject As String, DetailForDeleteXml As String, CodeUser As String) As SP_SaveProvisionAndDeterioration_Result Implements IPortfolioProvisionRepository.SP_SaveProvisionAndDeterioration
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveProvisionAndDeterioration(XmlObject, DetailForDeleteXml, CodeUser).SingleOrDefault
    End Function

    Public Function SP_ConfirmPortfolioProvision(PortfolioProvisionId As Integer, CodeUser As String, Optional operativeUnitId As Integer? = Nothing) As SP_ConfirmProvisionAndDeterioration_Result Implements IPortfolioProvisionRepository.SP_ConfirmPortfolioProvision
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmProvisionAndDeterioration(PortfolioProvisionId, CodeUser, operativeUnitId).SingleOrDefault
    End Function

    ''' <summary>
    ''' Obtiene y calcula la información del Deterioro de Cartera de acuerdo a la Clasificación
    ''' </summary>
    ''' <param name="closingDate"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <returns></returns>
    Public Function SP_GetPortfolioDeteriorationByClassification(closingDate As Date, operativeUnitId As Integer) As List(Of PortfolioDeteriorationByClassificationDTO) Implements IPortfolioProvisionRepository.SP_GetPortfolioDeteriorationByClassification
        Dim params = New List(Of (String, Object)) From {
                ("@ClosingDate", closingDate),
                ("@OperativeUnitId", operativeUnitId)
            }
        Dim query = Me.ExecuteStoredProcedure(Of PortfolioDeteriorationByClassificationDTO)("[Portfolio].[SP_GetPortfolioDeteriorationByClassification]", params)
        Return If(query?.ToList(), New List(Of PortfolioDeteriorationByClassificationDTO)())
    End Function



#End Region

End Class
