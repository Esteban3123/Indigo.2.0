'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class RecognitionRepository
    Inherits GenericRepository(Of Recognition)
    Implements IRecognitionRepository

    'Contexto 
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un reconocimiento por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecognition(Code As String, ItemType As Byte, budgetaryValidityId As Integer) As Recognition Implements IRecognitionRepository.GetRecognition

        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim query = (From d As Recognition In Me._context.Recognition.Include("RecognitionDetail")
                     Where d.Code.Equals(Code.Trim()) And d.BudgetaryValidityId = budgetaryValidityId Select d).FirstOrDefault

        If query IsNot Nothing Then

            query.OriginalValue = (From d As Recognition In Me._context.Recognition.AsNoTracking Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault

            Dim tp = (From t In Me._context.ThirdParty.AsNoTracking() Where query.ThirdPartyId = t.Id Select t).FirstOrDefault()
            query.NameThirdParty = tp.Nit + " - " + tp.Name

            Dim dp = (From dy In Me._context.Dependency.AsNoTracking() Where query.DependencyId = dy.Id Select dy).FirstOrDefault()
            query.NameDependency = dp.Code + " - " + dp.Name

            If query.RecognitionDetail IsNot Nothing AndAlso query.RecognitionDetail.Count > 0 Then
                For Each item In query.RecognitionDetail

                    Dim budget = (From b In Me._context.Budget.AsNoTracking Where b.CategoryId = item.CategoryId And b.RevenueTypeId = item.RevenueTypeId Select b).FirstOrDefault()
                    item.ValueBalance = budget.Balance

                    Dim ct = (From c In Me._context.Category.AsNoTracking Where c.Id = budget.CategoryId Select c).FirstOrDefault()
                    item.CategoryId = ct.Id
                    item.CodeCategory = ct.Code
                    item.NameCategory = ct.Name

                    Dim revenueType = (From rt In Me._context.RevenueType.AsNoTracking Where rt.Id = budget.RevenueTypeId Select rt).FirstOrDefault()
                    item.CodeNameRevenueType = revenueType.Code + " - " + revenueType.Name

                    Dim financialSource = (From fs In Me._context.FinancialSource.AsNoTracking Where fs.Id = ct.FinancialSourceId Select fs).FirstOrDefault()
                    item.FinancialSourceId = financialSource.Id
                    item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name


                Next

            End If

            Return query
        Else
            Return New Recognition()
        End If

    End Function

    ''' <summary>
    ''' Obtiene un reconocimiento por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecognitionById(Id As Integer) As Recognition Implements IRecognitionRepository.GetRecognitionById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.Recognition.Include("RecognitionDetail") Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As Recognition In Me._context.Recognition.AsNoTracking() Where d.Id = Id Select d).FirstOrDefault

            Return res
        Else
            Return New Recognition
        End If
    End Function
#End Region

    ''' <summary>
    ''' Crea un recaudo con un store procedure y enviando el objeto como Xml
    ''' </summary>
    ''' <param name="recognitionXml"></param>
    ''' <param name="recognitionDetailForDeleteXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveRecognition(recognitionXml As String, recognitionDetailForDeleteXml As String, codeUser As String) As SP_SaveRecognition_Result Implements IRecognitionRepository.SaveRecognition
        Return _context.SP_SaveRecognition(recognitionXml, recognitionDetailForDeleteXml, codeUser).SingleOrDefault
    End Function

End Class
