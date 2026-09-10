'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DefinitionRateRepository
    Inherits GenericRepository(Of DefinitionRate)
    Implements IDefinitionRateRepository

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
    ''' Obtiene una definicion de tarifa por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDefinitionRate(code As String) As DefinitionRate Implements IDefinitionRateRepository.GetDefinitionRate
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As DefinitionRate In Me._context.DefinitionRate
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault()

        Return If(res, New DefinitionRate())
    End Function

    ''' <summary>
    ''' Obtiene una definicion de tarifa por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDefinitionRateById(id As Integer) As DefinitionRate Implements IDefinitionRateRepository.GetDefinitionRateById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.DefinitionRate Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As DefinitionRate In Me._context.DefinitionRate.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New DefinitionRate()
        End If
    End Function

    '' <summary>
    '' Metodo que obtiene una lista de codigos de definicion de tarifa que tienen asociados procedimientos quirurgicos. 
    '' </summary>
    '' <param name="listSurgicalProcedureServiceId"></param>
    '' <returns></returns>
    Function GetListDefinitionRateCode(listSurgicalProcedureServiceId As List(Of Integer)) As List(Of String) Implements IDefinitionRateRepository.GetListDefinitionRateCode

        Dim listDefinitionRateCode As New List(Of String)
        If (listSurgicalProcedureServiceId.Any()) Then
            Dim definitionRateCode = (From sp In _context.DefinitionRateDetailSurgicalProcedures.AsNoTracking()
                                      Join drd In _context.DefinitionRateDetail.AsNoTracking() On sp.DefinitionRateDetailId Equals drd.Id
                                      Join dr In _context.DefinitionRate.AsNoTracking() On drd.DefinitionRateId Equals dr.Id
                                      Where listSurgicalProcedureServiceId.Contains(sp.SurgicalProcedureServiceId)
                                      Group By dr.Code Into Group
                                      Select Code).ToList()
            If definitionRateCode?.Any() Then
                listDefinitionRateCode.AddRange(definitionRateCode)
            End If
        End If
        Return listDefinitionRateCode
    End Function

End Class
