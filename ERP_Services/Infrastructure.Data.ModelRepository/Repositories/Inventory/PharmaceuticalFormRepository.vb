'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class PharmaceuticalFormRepository
    Inherits GenericRepository(Of PharmaceuticalForm)
    Implements IPharmaceuticalFormRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una forma farmaceutica por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmaceuticalForm(code As String) As PharmaceuticalForm Implements IPharmaceuticalFormRepository.GetPharmaceuticalForm
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As PharmaceuticalForm In Me._context.PharmaceuticalForm
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.PharmaceuticalForm.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New PharmaceuticalForm()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una forma farmaceutica por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmaceuticalFormById(id As Integer) As PharmaceuticalForm Implements IPharmaceuticalFormRepository.GetPharmaceuticalFormById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.PharmaceuticalForm Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As PharmaceuticalForm In Me._context.PharmaceuticalForm.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New PharmaceuticalForm()
        End If
    End Function

    Public Function SavePharmaceuticalForm(xml As String, CodeUser As String) As SP_SavePharmaceuticalForm_Result Implements IPharmaceuticalFormRepository.SavePharmaceuticalForm
        Return _context.SP_SavePharmaceuticalForm(xml, CodeUser).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function SP_DeletePharmaceuticalForm(Id As Integer) As SP_DeletePharmaceuticalForm_Result Implements IPharmaceuticalFormRepository.SP_DeletePharmaceuticalForm
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeletePharmaceuticalForm(Id).SingleOrDefault
    End Function

End Class
