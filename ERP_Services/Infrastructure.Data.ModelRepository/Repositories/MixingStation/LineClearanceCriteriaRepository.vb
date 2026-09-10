'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Andres Alarcon
' Created          : 21/02/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

#End Region

Public Class LineClearanceCriteriaRepository
    Inherits GenericRepository(Of LineClearanceCriteria)
    Implements ILineClearanceCriteriaRepository, Inject

    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un criterio de despeje de linea mediante el codigo
    ''' </summary>
    ''' <param name="code">Codigo del tipo de dosis unitaria</param>
    ''' <returns>Tipo de Dosis Unitaria</returns>
    ''' <remarks></remarks>
    Public Function GetLineClearanceCriteriaByCode(code As String, Optional tracking As Boolean = True) As LineClearanceCriteria Implements ILineClearanceCriteriaRepository.GetLineClearanceCriteriaByCode
        Dim _lineClearanceCriteria As LineClearanceCriteria = Nothing
        If tracking Then
            _lineClearanceCriteria = (From e In _context.LineClearanceCriteria.Include("LineClearanceCriteriaUnitDoseType")
                                      Where e.Code = code
                                      Select e).FirstOrDefault
        Else
            _lineClearanceCriteria = (From e In _context.LineClearanceCriteria.AsNoTracking.Include("LineClearanceCriteriaUnitDoseType").AsNoTracking
                                      Where e.Code = code
                                      Select e).FirstOrDefault
        End If

        If _lineClearanceCriteria IsNot Nothing Then

            If _lineClearanceCriteria.LineClearanceCriteriaUnitDoseType?.Any() Then
                For Each item In _lineClearanceCriteria.LineClearanceCriteriaUnitDoseType
                    Dim unitDoseType = (From x In _context.UnitDoseType.AsNoTracking
                                        Where x.Id = item.UnitDoseTypeId
                                        Select x).FirstOrDefault()

                    item.UnitDoseTypeCode = unitDoseType.Code
                    item.UnitDoseTypeName = unitDoseType.Description
                Next
            End If

            Return _lineClearanceCriteria
        Else
            Return New LineClearanceCriteria()
        End If

    End Function
End Class
