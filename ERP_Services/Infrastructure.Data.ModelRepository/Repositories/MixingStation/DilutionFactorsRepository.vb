'***********************************************************************
' Assembly         : Infrastructure.Data.BatchSerialSequenceRepository
' Author           : Giovanny Plazas
' Created          : 25-08-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
Imports System.Text
Imports System.Data.Entity


#End Region

Public Class DilutionFactorsRepository
    Inherits GenericRepository(Of DilutionFactors)
    Implements IDilutionFactorsRepository, Inject

#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub

#End Region

    ''' <summary>
    ''' Valda si el medicamento que se escogio ya esta parametrizado para otro factor de dilucion
    ''' </summary>
    ''' <param name="DilutionFactor"></param>
    ''' <returns></returns>
    Public Async Function ValidateDuplicateDilutionFactorsAsync(DilutionFactor As DilutionFactors) As Task(Of String) Implements IDilutionFactorsRepository.ValidateDuplicateDilutionFactorsAsync

        Dim DilutionFactorRepeat = Await (From t1 In _context.DilutionFactors.AsNoTracking()
                                          Where t1.ATCId = DilutionFactor.ATCId And DilutionFactor.Id <> t1.Id
                                          Select t1.Code).FirstOrDefaultAsync

        Dim messageReturn As New StringBuilder
        If Not String.IsNullOrEmpty(DilutionFactorRepeat) Then
            messageReturn.AppendLine("El factor de dilución con codigo " + DilutionFactorRepeat + " ya tiene parametrizado el mismo medicamento")
        End If

        Return messageReturn.ToString()
    End Function


End Class
