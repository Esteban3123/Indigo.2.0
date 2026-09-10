'************************************************************
' Assembly         : Domain.Entities
' Author           : Andrés Steven Rojas
' Created          : 07-01-2025
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class RejectionReasonRepository
    Inherits GenericRepository(Of RejectionReason)
    Implements IRejectionReasonRepository, Inject

    ''' <summary>
    ''' Contexto de la entidades del modelo
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' Lista los Motivos de Rechazo de acuerdo al código de usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function ListRejectionReasonByUserCode(userCode As String) As List(Of RejectionReason) Implements IRejectionReasonRepository.ListRejectionReasonByUserCode
        Return (From r As RejectionReason In Me._context.RejectionReason.Include("RejectionReasonUser")
                Where r.Status And r.RejectionReasonUser.Any(Function(u) u.Usercode.Equals(userCode))
                Select r).ToList()
    End Function
    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetRejectionReasonById(id As Integer) As RejectionReason Implements IRejectionReasonRepository.GetRejectionReasonById
        Return (From r As RejectionReason In Me._context.RejectionReason
                Where r.Id = id
                Select r).FirstOrDefault()
    End Function
    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por Código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetRejectionReasonByCode(code As String) As RejectionReason Implements IRejectionReasonRepository.GetRejectionReasonByCode
        Return (From r As RejectionReason In Me._context.RejectionReason.Include("RejectionReasonUser")
                Where r.Code = code.Trim()
                Select r).FirstOrDefault()
    End Function
End Class
