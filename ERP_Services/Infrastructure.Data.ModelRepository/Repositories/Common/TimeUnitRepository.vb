'***********************************************************************
' Assembly         : Infrastructure.Data.CommonRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities


Public Class TimeUnitRepository
    Inherits GenericRepository(Of TimeUnit)

    Implements ITimeUnitRepository


    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un Tipo de Unidad de Tiempo
    ''' </summary>
    ''' <param name="code">Código de Unidad de Tiempo</param>
    ''' <returns>Unidad de Tiempo</returns>
    ''' <remarks></remarks>
    Public Function GetTimeUnit(code As String) As TimeUnit Implements ITimeUnitRepository.GetTimeUnit
        Dim _timeUnit = From e In _context.TimeUnit
                    Where e.Code = code
                    Select e

        If _timeUnit.Count > 0 Then
            _timeUnit.FirstOrDefault.OriginalValue = (From e In _context.TimeUnit.AsNoTracking
                                               Where e.Code = code
                                               Select e).FirstOrDefault
            Return _timeUnit.FirstOrDefault()
        Else
            Return New TimeUnit()
        End If

    End Function

    ''' <summary>
    ''' Lista todas las Unidades de Tiempo
    ''' </summary>
    ''' <returns>Unidades de Tiempo</returns>
    ''' <remarks></remarks>
    Public Function ListAllTimeUnit() As List(Of TimeUnit) Implements ITimeUnitRepository.ListAllTimeUnit
        Dim timeUnit = From e In _context.TimeUnit
                       Select e
        Return timeUnit.ToList()
    End Function


    ''' <summary>
    ''' funcion para consultar la unidad de tiempo por id
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="desatach"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTimeUnitByCode(code As String, Optional desatach As Boolean = True) As TimeUnit Implements ITimeUnitRepository.GetTimeUnitByCode
        Dim costCenter = From e In _context.TimeUnit
                 Where e.Code = code
                 Select e
        If costCenter.Count > 0 Then
            Dim objCostCenter = Nothing
            If desatach = False Then
                objCostCenter = (From e In _context.TimeUnit.AsNoTracking
                                 Where e.Code = code
                                 Select e).SingleOrDefault
            Else
                objCostCenter = costCenter.SingleOrDefault()
            End If
            Return objCostCenter
        Else
            Return New TimeUnit
        End If
    End Function
End Class
