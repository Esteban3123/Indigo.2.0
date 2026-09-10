'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/12/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class VieBotRepository
    Inherits GenericRepository(Of VieBot)
    Implements IVieBotRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Functions"

    ''' <summary>
    ''' Obtiene listado de configuración de VieBot por formulario
    ''' </summary>
    ''' <param name="Form"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVieBotByForm(Form As String, Optional tracking As Boolean = True) As List(Of VieBot) Implements IVieBotRepository.GetVieBotByForm
        If Form Is Nothing OrElse Form.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Form")
        End If
        Dim res As List(Of VieBot)
        If tracking Then
            res = (From d As VieBot In Me._context.VieBot Where d.Form.Equals(Form.Trim()) Select d).ToList
        Else
            res = (From d As VieBot In Me._context.VieBot.AsNoTracking Where d.Form.Equals(Form.Trim()) Select d).ToList

            If res IsNot Nothing Then
                For Each item In res
                    Dim LegalBook = (From l In _context.LegalBook.AsNoTracking Where l.Id = item.LegalBookId Select l).FirstOrDefault
                    item.CodeNameLegalBook = LegalBook.Code + " - " + LegalBook.Name
                    item.IsOfficialBook = LegalBook.OfficialBook
                    item.OfficialCurrencyId = LegalBook.OfficialCurrencyId
                Next
            End If
        End If

        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return Nothing
        End If
    End Function

#End Region

End Class
