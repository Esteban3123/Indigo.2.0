'***********************************************************************
' Assembly         : Presentacion.AccoungManagement
' Author           : Andres Felipe Quintero Garcia 
' Created          : 09-01-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Data.Entity
Imports System.Diagnostics
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class AccountManagementParametersRepository
    Inherits GenericRepository(Of AccountManagementParameters)
    Implements IAccountManagementParametersRepository, Inject

#Region "Constantes"
    ' Tipos de Ingreso
    Private Const ENTRY_TYPE_AMBULATORIO As String = "1"
    Private Const ENTRY_TYPE_HOSPITALARIO As String = "2"
    Private Const ENTRY_TYPE_AMBOS As String = "3"
    Private Const ENTRY_TYPE_AMBULATORIO_NAME As String = "Ambulatorio"
    Private Const ENTRY_TYPE_HOSPITALARIO_NAME As String = "Hospitalario"
    Private Const ENTRY_TYPE_AMBOS_NAME As String = "Hospitalario y Ambulatorio"

    ' Clases de Cama
    Private Const BED_CLASS_OBSERVACION_URGENCIAS As String = "1"
    Private Const BED_CLASS_RECUPERACION_POSTQUIRURGICO As String = "2"
    Private Const BED_CLASS_HOSPITALARIO As String = "3"
    Private Const BED_CLASS_CUNA_OBSERVACION As String = "4"
    Private Const BED_CLASS_OBSERVACION_URGENCIAS_NAME As String = "Observacion Urgencias"
    Private Const BED_CLASS_RECUPERACION_POSTQUIRURGICO_NAME As String = "Recuperacion Post-Quirurgico"
    Private Const BED_CLASS_HOSPITALARIO_NAME As String = "Hospitalario"
    Private Const BED_CLASS_CUNA_OBSERVACION_NAME As String = "Cuna de Observación"
    Private Const NOT_AVAILABLE As String = "N/A"
#End Region

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
    ''' Obtiene el parametro de gestión de cuentas por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns>AccountManagementParameter con los UsersAssignment y UserNovelties ligados</returns>
    Public Function GetAccountManagementParametersById(id As Integer) As AccountManagementParameters Implements IAccountManagementParametersRepository.GetAccountManagementParametersById
        If id <= 0 Then
            Throw New ArgumentException("El Id debe ser mayor a cero", NameOf(id))
        End If

        ' Obtener el parámetro principal
        Dim query = (From tc In _context.AccountManagementParameters Where tc.Id = id Select tc).FirstOrDefault()

        ' Si no se encuentra el parámetro, retornar uno nuevo
        If query Is Nothing Then
            Return New AccountManagementParameters()
        End If

        ' Cargar usuarios asignados
        Dim assignedUsers = _context.UsersAssignment.AsNoTracking().Where(Function(u) u.IsRemoved = False).ToList()

        ' Inicializar colección de usuarios asignados
        query.usersAssignment = New List(Of UsersAssignment)

        ' Mapear nombres de tipo de ingreso para usuarios y asignarlos
        If assignedUsers IsNot Nothing AndAlso assignedUsers.Any() Then
            For Each user In assignedUsers
                user.EntryTypeName = GetEntryTypeName(user.EntryType)
            Next
            query.usersAssignment = assignedUsers
        End If

        ' Cargar novedades de usuarios
        Dim userNovelties = _context.UserNovelties.AsNoTracking().ToList()

        ' Inicializar colección de novedades
        query.userNovelties = New List(Of UserNovelties)

        ' Asignar novedades si existen
        If userNovelties IsNot Nothing AndAlso userNovelties.Any() Then
            query.userNovelties = userNovelties
        End If

        ' Mapear nombres de tipos de ingreso del parámetro
        If Not String.IsNullOrEmpty(query.EntryType) Then
            query.EntryTypeName = MapEntryTypeNames(query.EntryType)
        End If

        ' Mapear nombres de clases de cama
        If Not String.IsNullOrEmpty(query.BedClass) Then
            query.BedClassName = MapBedClassNames(query.BedClass)
        End If

        Return query
    End Function

    ''' <summary>
    ''' Mapea el tipo de ingreso numérico a su nombre descriptivo para usuarios
    ''' </summary>
    ''' <param name="entryType">Tipo de ingreso numérico</param>
    ''' <returns>Nombre descriptivo del tipo de ingreso</returns>
    Private Function GetEntryTypeName(entryType As Integer) As String
        Select Case entryType
            Case 1
                Return ENTRY_TYPE_AMBULATORIO_NAME
            Case 2
                Return ENTRY_TYPE_HOSPITALARIO_NAME
            Case 3
                Return ENTRY_TYPE_AMBOS_NAME
            Case Else
                Return NOT_AVAILABLE
        End Select
    End Function

    ''' <summary>
    ''' Mapea una lista separada por comas de tipos de ingreso a sus nombres descriptivos
    ''' </summary>
    ''' <param name="entryTypes">String con tipos de ingreso separados por comas</param>
    ''' <returns>String con nombres descriptivos separados por comas</returns>
    Private Function MapEntryTypeNames(entryTypes As String) As String
        If String.IsNullOrWhiteSpace(entryTypes) Then
            Return String.Empty
        End If

        Dim names As New List(Of String)
        Dim listEntryType = entryTypes.Split(","c)

        For Each item In listEntryType
            Dim trimmedItem = item.Trim()
            Select Case trimmedItem
                Case ENTRY_TYPE_AMBULATORIO
                    names.Add(ENTRY_TYPE_AMBULATORIO_NAME)
                Case ENTRY_TYPE_HOSPITALARIO
                    names.Add(ENTRY_TYPE_HOSPITALARIO_NAME)
                Case Else
                    names.Add(NOT_AVAILABLE)
            End Select
        Next

        Return String.Join(", ", names)
    End Function

    ''' <summary>
    ''' Mapea una lista separada por comas de clases de cama a sus nombres descriptivos
    ''' </summary>
    ''' <param name="bedClasses">String con clases de cama separados por comas</param>
    ''' <returns>String con nombres descriptivos separados por comas</returns>
    Private Function MapBedClassNames(bedClasses As String) As String
        If String.IsNullOrWhiteSpace(bedClasses) Then
            Return String.Empty
        End If

        Dim names As New List(Of String)
        Dim listBedClass = bedClasses.Split(","c)

        For Each item In listBedClass
            Dim trimmedItem = item.Trim()
            Select Case trimmedItem
                Case BED_CLASS_OBSERVACION_URGENCIAS
                    names.Add(BED_CLASS_OBSERVACION_URGENCIAS_NAME)
                Case BED_CLASS_RECUPERACION_POSTQUIRURGICO
                    names.Add(BED_CLASS_RECUPERACION_POSTQUIRURGICO_NAME)
                Case BED_CLASS_HOSPITALARIO
                    names.Add(BED_CLASS_HOSPITALARIO_NAME)
                Case BED_CLASS_CUNA_OBSERVACION
                    names.Add(BED_CLASS_CUNA_OBSERVACION_NAME)
                Case Else
                    names.Add(NOT_AVAILABLE)
            End Select
        Next

        Return String.Join(", ", names)
    End Function

    ''' <summary>
    ''' Obtiene los usuarios disponibles para asignación de ingresos
    ''' </summary>
    ''' <returns>Lista de usuarios disponibles para asignación de ingresos</returns>
    Public Function GetAvailableUsers() As List(Of UsersAssignment) Implements IAccountManagementParametersRepository.GetAvailableUsers
        Dim availableUsers = (From au In _context.UsersAssignment.AsNoTracking Where au.Status = True Select au).ToList()
        Return availableUsers
    End Function

    ''' <summary>
    ''' Obtiene los usuarios con ingresos distribuidos o con transferencias de folios pendientes
    ''' </summary>
    ''' <returns>Lista de usuarios con ingresos distribuidos o transferencias de folios en estado 2</returns>
    Public Async Function GetUsersWithDistributedAccounts() As Task(Of List(Of UsersAssignment)) Implements IAccountManagementParametersRepository.GetUsersWithDistributedAccounts
        ' Primera consulta: Usuarios con distribuciones automáticas de ingresos
        Dim usersWithDistributions = From au In _context.UsersAssignment.AsNoTracking
                                     Join aed In _context.AutomaticEntryDistribution.AsNoTracking On au.Id Equals aed.AssignedUserId
                                     Select au

        ' Segunda consulta: Usuarios con transferencias de folios en estado 2
        Dim usersWithFolioTransfers = From ua In _context.UsersAssignment.AsNoTracking
                                      Join ft In _context.FolioTransfer.AsNoTracking On ua.UserCode Equals ft.ReceivingUser
                                      Where ft.TransferStatus = 2
                                      Select ua

        ' UNION de ambas consultas y eliminar duplicados
        Dim combinedUsers = Await usersWithDistributions.Union(usersWithFolioTransfers).Distinct().ToListAsync()

        Return combinedUsers
    End Function


End Class
