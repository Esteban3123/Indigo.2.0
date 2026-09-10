'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Cesar Collazos
' Created          : 01/12/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

''' <summary>
''' Repositorio de acompañantes/responsables de ingreso (ADACOMPAN)
''' </summary>
Public Class AdacompanRepository
    Inherits GenericRepository(Of ADACOMPAN)
    Implements IAdacompanRepository

#Region "Constants"

    ''' <summary>
    ''' Valor que indica responsable en ADACOMPAN (RESPONSAB = '2')
    ''' </summary>
    Private Const RESPONSABLE_VALUE As String = "2"

#End Region

#Region "Fields"

    ''' <summary>
    ''' Contexto del repositorio
    ''' </summary>
    Private ReadOnly _context As IGlobalModelUnitOfWork

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor del repositorio
    ''' </summary>
    ''' <param name="context">Contexto del repositorio</param>
    Public Sub New(context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Obtiene el responsable de un ingreso específico
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <returns>Entidad ADACOMPAN del responsable si existe, Nothing en caso contrario</returns>
    ''' <remarks>
    ''' Busca en la tabla ADACOMPAN donde RESPONSAB = '2' (responsable) para el ingreso dado.
    ''' Si hay múltiples responsables, retorna el más reciente por FECHAREGI.
    ''' </remarks>
    Public Function GetResponsibleByAdmissionNumber(admissionNumber As String) As ADACOMPAN Implements IAdacompanRepository.GetResponsibleByAdmissionNumber
        If String.IsNullOrWhiteSpace(admissionNumber) Then
            Return Nothing
        End If

        Dim trimmedAdmission = admissionNumber.Trim()

        Dim responsible = (From a In _context.ADACOMPAN.AsNoTracking()
                           Where a.NUMINGRES IsNot Nothing AndAlso
                                 a.NUMINGRES.Trim() = trimmedAdmission AndAlso
                                 a.RESPONSAB = RESPONSABLE_VALUE
                           Order By a.FECHAREGI Descending
                           Select a).FirstOrDefault()

        Return responsible
    End Function

    ''' <summary>
    ''' Obtiene todos los acompañantes de un ingreso específico
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <returns>Lista de acompañantes del ingreso</returns>
    Public Function GetByAdmissionNumber(admissionNumber As String) As List(Of ADACOMPAN) Implements IAdacompanRepository.GetByAdmissionNumber
        If String.IsNullOrWhiteSpace(admissionNumber) Then
            Return New List(Of ADACOMPAN)()
        End If

        Dim trimmedAdmission = admissionNumber.Trim()

        Dim companions = (From a In _context.ADACOMPAN.AsNoTracking()
                          Where a.NUMINGRES IsNot Nothing AndAlso
                                a.NUMINGRES.Trim() = trimmedAdmission
                          Order By a.FECHAREGI Descending
                          Select a).ToList()

        Return companions
    End Function

    ''' <summary>
    ''' Obtiene todos los acompañantes de un paciente específico
    ''' </summary>
    ''' <param name="patientCode">Código del paciente (IPCODPACI)</param>
    ''' <returns>Lista de acompañantes del paciente</returns>
    Public Function GetByPatientCode(patientCode As String) As List(Of ADACOMPAN) Implements IAdacompanRepository.GetByPatientCode
        If String.IsNullOrWhiteSpace(patientCode) Then
            Return New List(Of ADACOMPAN)()
        End If

        Dim trimmedPatientCode = patientCode.Trim()

        Dim companions = (From a In _context.ADACOMPAN.AsNoTracking()
                          Where a.IPCODPACI IsNot Nothing AndAlso
                                a.IPCODPACI.Trim() = trimmedPatientCode
                          Order By a.FECHAREGI Descending
                          Select a).ToList()

        Return companions
    End Function

    ''' <summary>
    ''' Busca un acompañante por su identificación
    ''' </summary>
    ''' <param name="identification">Identificación del acompañante (IDACOMPAN)</param>
    ''' <returns>Entidad ADACOMPAN si existe, Nothing en caso contrario</returns>
    Public Function GetByIdentification(identification As String) As ADACOMPAN Implements IAdacompanRepository.GetByIdentification
        If String.IsNullOrWhiteSpace(identification) Then
            Return Nothing
        End If

        Dim trimmedIdentification = identification.Trim()

        Dim companion = (From a In _context.ADACOMPAN.AsNoTracking()
                         Where a.IDACOMPAN IsNot Nothing AndAlso
                               a.IDACOMPAN.Trim() = trimmedIdentification
                         Order By a.FECHAREGI Descending
                         Select a).FirstOrDefault()

        Return companion
    End Function

    ''' <summary>
    ''' Verifica si existe un responsable para un ingreso específico
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <returns>True si existe un responsable, False en caso contrario</returns>
    Public Function HasResponsible(admissionNumber As String) As Boolean Implements IAdacompanRepository.HasResponsible
        If String.IsNullOrWhiteSpace(admissionNumber) Then
            Return False
        End If

        Dim trimmedAdmission = admissionNumber.Trim()

        Return (From a In _context.ADACOMPAN.AsNoTracking()
                Where a.NUMINGRES IsNot Nothing AndAlso
                      a.NUMINGRES.Trim() = trimmedAdmission AndAlso
                      a.RESPONSAB = RESPONSABLE_VALUE
                Select a).Any()
    End Function

    ''' <summary>
    ''' Obtiene todos los acompañantes por tipo de parentesco
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="relationshipType">Tipo de parentesco (PARACOMPA)</param>
    ''' <returns>Lista de acompañantes con el tipo de parentesco especificado</returns>
    Public Function GetByRelationshipType(admissionNumber As String, relationshipType As String) As List(Of ADACOMPAN) Implements IAdacompanRepository.GetByRelationshipType
        If String.IsNullOrWhiteSpace(admissionNumber) OrElse String.IsNullOrWhiteSpace(relationshipType) Then
            Return New List(Of ADACOMPAN)()
        End If

        Dim trimmedAdmission = admissionNumber.Trim()
        Dim trimmedRelationship = relationshipType.Trim()

        Dim companions = (From a In _context.ADACOMPAN.AsNoTracking()
                          Where a.NUMINGRES IsNot Nothing AndAlso
                                a.NUMINGRES.Trim() = trimmedAdmission AndAlso
                                a.PARACOMPA IsNot Nothing AndAlso
                                a.PARACOMPA.Trim() = trimmedRelationship
                          Order By a.FECHAREGI Descending
                          Select a).ToList()

        Return companions
    End Function

#End Region

End Class

