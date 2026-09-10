'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 15/04/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class CMCenterAttentionRepository
    Inherits GenericRepository(Of CMCenterAttention)
    Implements ICMCenterAttentionRepository, Inject
    ''' <summary>
    ''' Contexto de Configuración de Central de Mezclas
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Configuración de Central de Mezclas
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
    ''' <summary>
    ''' Lista todos los parametros de configuración de central de Mezclas
    ''' </summary>
    ''' <returns>Lista de parametros</returns>
    ''' <remarks></remarks>
    Public Function ListAllCMCenterAttention() As List(Of CMCenterAttention) Implements ICMCenterAttentionRepository.ListAllCMCenterAttention
        Dim cmCenterAttention = From e In _context.CMCenterAttention
                                Select e
        Return cmCenterAttention.ToList()
    End Function
    ''' <summary>
    ''' Obtiene los parametros de cental de mezclas por Id
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetCMCenterAttention(id As String, Optional tracking As Boolean = True) As CMCenterAttention Implements ICMCenterAttentionRepository.GetCMCenterAttention
        Dim cmCenterAttention = From e In _context.CMCenterAttention
                                Select e
        If cmCenterAttention.Count > 0 Then
            Dim ObjcmCenterAttention = Nothing
            If tracking = False Then
                ObjcmCenterAttention = (From e In _context.CMCenterAttention.AsNoTracking
                                        Select e).SingleOrDefault
            Else
                ObjcmCenterAttention = cmCenterAttention.SingleOrDefault
            End If
            Return ObjcmCenterAttention
        Else
            Return New CMCenterAttention()
        End If
    End Function

End Class
