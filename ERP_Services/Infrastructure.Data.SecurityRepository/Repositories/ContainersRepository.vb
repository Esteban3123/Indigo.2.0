'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Juan Diego Diaz M.
' Created          : 06-02-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Security
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Interface
Imports System.Configuration
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Repositorio para Contenedores
''' </summary>
Public Class ContainersRepository
    Implements IContainersRepository

    'Devuelve el contexto en este repositorio 
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia de <see cref="GroupRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función para obtener una lista de contenedores
    ''' </summary>
    ''' <returns>Lista de Contenedores</returns>
    Public Function getContainers() As List(Of Containers) Implements IContainersRepository.getContainers
        Dim source = (From a In _context.Containers
                      Select a).ToList

        If source Is Nothing OrElse source.Count = 0 Then
            Return New List(Of Containers)()
        End If

        Dim tenantInfo = (From tc In _context.TenantContainer
                          Join t In _context.Tenant On tc.TenantId Equals t.Id
                          Select tc.ContainerId, tc.TenantId, TenantName = t.Name).ToList
        Dim result = From c In source
                     Group Join ti In tenantInfo On c.Id Equals ti.ContainerId Into matches = Group
                     From ti In matches.DefaultIfEmpty()
                     Select New Containers() With {
                        .Id = c.Id,
                        .Code = c.Code,
                        .Name = c.Name,
                        .TransactionalContainer = c.TransactionalContainer,
                        .ProductionCompany = c.ProductionCompany,
                        .TenantId = If(ti Is Nothing, CShort(0), ti.TenantId),
                        .TenantName = If(ti Is Nothing, String.Empty, ti.TenantName)
                     }

        Return result.ToList
    End Function

    ''' <summary>
    ''' Obtiene el nombre del contenedor de seguridad
    ''' </summary>
    ''' <returns>Nombre del contenedor de seguridad</returns>
    Public Function getSecurityContainerName() As String Implements IContainersRepository.getSecurityContainerName
        Dim containerName = System.Configuration.ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME)
        If containerName IsNot Nothing Then
            Return containerName.ToString()
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Función para obtener un contenedor
    ''' </summary>
    ''' <returns>Objeto Contenedor</returns>
    Public Function getContainersByName(Name As String) As Containers Implements IContainersRepository.getContainersByName
        Dim result = (From a In _context.Containers
                      Where a.Name = Name
                      Select a).FirstOrDefault

        If result IsNot Nothing Then
            Return result
        Else
            Return New Containers
        End If
    End Function

    ''' <summary>
    ''' Obtiene los contenedores para el codigo de empresa dado
    ''' </summary>
    ''' <param name="code">Codigo de empresa a consultar</param>
    ''' <returns>Contenedores</returns>
    Public Function getContainersByCode(code As String) As Containers Implements IContainersRepository.getContainersByCode
        Dim result = (From a In _context.Containers
                      Where a.Code = code
                      Select a).FirstOrDefault

        If result IsNot Nothing Then
            Return result
        Else
            Return New Containers
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idCompany"></param>
    ''' <returns></returns>
    Public Function getContainersById(ByVal idCompany As Integer) As Containers Implements IContainersRepository.getContainersById
        Dim result = (From a In _context.Containers
                      Where a.Id = idCompany
                      Select a).FirstOrDefault

        If result IsNot Nothing Then
            Return result
        Else
            Return New Containers
        End If
    End Function

    ''' <summary>
    ''' Obtiene el nombre del contenedor de Indigo Vie Cloud Platform
    ''' </summary>
    ''' <returns>Nombre del contenedor de Indigo Vie Cloud Platform</returns>
    Public Function getInteropCostContainerName() As String Implements IContainersRepository.getInteropCostContainerName
        Dim containerName = System.Configuration.ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.INTEROP_COST_CONTAINER_PARAMETER_NAME)
        If containerName IsNot Nothing Then
            Return containerName.ToString()
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

	Public Function ListCompanies() As List(Of Company) Implements IContainersRepository.ListCompanies
		Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
			Dim query = "
                    SELECT 
	                    C.Id, C.Code, C.Name, C.FoundationalContainer, C.TransactionalContainer, C.DocumentalContainer, C.VituelContainer, C.HISContainer, C.InteropCostContainer, C.HISIntegration,
	                    C.GlossesIntegration, C.PayrollIntegration, C.HumanTalentIntegration,C.DispensingIntegration, C.CompanyNit, C.Address, C.Telephone, C.ProductionCompany,
	                    C.LyncIntegration, C.Version, C.[State], C.CompanyType, C.VerificationDigitNit, C.ServiceConfigurationId, TC.TenantId, C.ArchitectureType, C.DecimalSeparator
                    FROM [Security].[Containers] C
                    LEFT OUTER JOIN [Security].TenantContainer TC ON TC.ContainerId = C.Id
                    WHERE C.[State] = 1"

			Dim dt As DataTable = conx.ExecuteCommand_Data(query)
			Dim res As List(Of Company) = New List(Of Company)()
			For Each r As DataRow In dt.Rows
				Dim company = New Company()
				company.Id = CInt(r("Id"))
				company.Code = r("Code").ToString()
				company.Name = r("Name").ToString()
				company.FoundationalContainer = r("FoundationalContainer").ToString()
				company.TransactionalContainer = r("TransactionalContainer").ToString()
				company.DocumentalContainer = r("DocumentalContainer").ToString()
				company.VituelContainer = r("VituelContainer").ToString()
				company.HISContainer = r("HISContainer").ToString()
				company.InteropCostContainer = r("InteropCostContainer").ToString()
				company.HISIntegration = CByte(r("HISIntegration"))
				company.GlossesIntegration = CByte(r("GlossesIntegration"))
				company.PayrollIntegration = CByte(r("PayrollIntegration"))
				company.HumanTalentIntegration = CByte(r("HumanTalentIntegration"))
				company.DispensingIntegration = CByte(r("DispensingIntegration"))
				company.CompanyType = CByte(r("CompanyType"))
				company.CompanyNit = r("CompanyNit").ToString()
				company.VerificationDigitNit = r("VerificationDigitNit").ToString().Trim()
				company.Address = r("Address").ToString()
				company.Telephone = r("Telephone").ToString()
				company.ProductionCompany = CBool(r("ProductionCompany"))
				company.State = CBool(r("State"))
				company.Version = r("Version").ToString()
				company.SecurityContainer = Me.getSecurityContainerName()
				company.TenantId = If(r("TenantId") Is DBNull.Value, CShort(0), CShort(r("TenantId")))
				company.ServiceConfigurationId = If(r("ServiceConfigurationId") Is DBNull.Value, CByte(0), CByte(r("ServiceConfigurationId")))
				company.ArchitectureType = If(r("ArchitectureType") Is DBNull.Value, CByte(1), CByte(r("ArchitectureType")))
				company.DecimalSeparator = r("DecimalSeparator").ToString()
				'company.InteropCostContainer = Me.getInteropCostContainerName()
				res.Add(company)
			Next
			Return res
		End Using
	End Function

	''' <summary>
	''' Lista las compañias que tiene permiso el usuario
	''' </summary>
	''' <param name="idUser"></param>
	''' <returns></returns>
	Public Function getCompaniesByUser(ByVal idUser As Integer) As List(Of Company) Implements IContainersRepository.getCompaniesByUser
		Dim scn As String = Me.getSecurityContainerName()
		Dim _User = (From u In _context.User
					 Where u.Id = idUser
					 Select u).FirstOrDefault

		Dim _Containers As IEnumerable(Of Company) = Nothing
        Select Case CType(_User.UserType, UserType)
            Case UserType.GlobalAdmin
                _Containers = From c In _context.Containers.Where(Function(c) c.State)
                              Join tc In _context.TenantContainer On tc.ContainerId Equals c.Id
                              Join t In _context.Tenant On t.Id Equals tc.TenantId
                              Join sc In _context.ServiceConfiguration On sc.Id Equals c.ServiceConfigurationId
                              Join co In _context.Countries On co.Id Equals t.CountryId
                              From u In _context.User.Where(Function(u) u.Id = idUser)
                              Join r In _context.Roll On r.Id Equals u.RollCode
                              Join g In _context.Group On g.Id Equals u.GroupCode
                              Select New Company() With {
                                .Id = c.Id,
                                .Code = c.Code,
                                .Name = c.Name,
                                .FoundationalContainer = c.FoundationalContainer,
                                .TransactionalContainer = c.TransactionalContainer,
                                .DocumentalContainer = c.DocumentalContainer,
                                .VituelContainer = c.VituelContainer,
                                .HISContainer = c.HISContainer,
                                .InteropCostContainer = c.InteropCostContainer,
                                .HISIntegration = c.HISIntegration,
                                .GlossesIntegration = c.GlossesIntegration,
                                .PayrollIntegration = c.PayrollIntegration,
                                .HumanTalentIntegration = c.HumanTalentIntegration,
                                .DispensingIntegration = c.DispensingIntegration,
                                .CompanyType = c.CompanyType,
                                .CompanyNit = c.CompanyNit,
                                .VerificationDigitNit = c.VerificationDigitNit.Trim,
                                .Address = c.Address,
                                .Telephone = c.Telephone,
                                .ProductionCompany = c.ProductionCompany,
                                .State = c.State,
                                .Version = c.Version,
                                .SecurityContainer = scn,
                                .TenantId = If(tc Is Nothing, CShort(0), tc.TenantId),
                                .TenantName = If(t Is Nothing, "", t.Name),
                                .ServiceConfigurationId = If(c.ServiceConfiguration Is Nothing, CByte(0), c.ServiceConfiguration.Id),
                                .Administrator = True,
                                .RollId = If(r Is Nothing, CInt(0), r.Id),
                                .RollCode = r.RollCode,
                                .RollName = r.Description,
                                .GroupId = If(g Is Nothing, CInt(0), g.Id),
                                .GroupCode = g.Code,
                                .GroupName = g.Description,
                                .Environment = If(c.ProductionCompany, "Producción", "Prueba"),
                                .CountryName = If(co Is Nothing, "", co.Name),
                                .Flagcode = If(co Is Nothing, "", co.Flagcode),
                                .ArchitectureType = If(c.ArchitectureType = 0, CByte(1), c.ArchitectureType),
                                .DecimalSeparator = c.DecimalSeparator,
                                .ServiceConfiguration = sc,
                                .ClientIdGuid = c.ClientId
                                }

            Case UserType.GlobalQa
                _Containers = From c In _context.Containers.Where(Function(c) c.State AndAlso Not c.ProductionCompany)
                              Join tc In _context.TenantContainer On tc.ContainerId Equals c.Id
                              Join t In _context.Tenant On t.Id Equals tc.TenantId
                              Join sc In _context.ServiceConfiguration On sc.Id Equals c.ServiceConfigurationId
                              Join co In _context.Countries On co.Id Equals t.CountryId
                              From u In _context.User.Where(Function(u) u.Id = idUser)
                              Join r In _context.Roll On r.Id Equals u.RollCode
                              Join g In _context.Group On g.Id Equals u.GroupCode
                              Select New Company() With {
                                .Id = c.Id,
                                .Code = c.Code,
                                .Name = c.Name,
                                .FoundationalContainer = c.FoundationalContainer,
                                .TransactionalContainer = c.TransactionalContainer,
                                .DocumentalContainer = c.DocumentalContainer,
                                .VituelContainer = c.VituelContainer,
                                .HISContainer = c.HISContainer,
                                .InteropCostContainer = c.InteropCostContainer,
                                .HISIntegration = c.HISIntegration,
                                .GlossesIntegration = c.GlossesIntegration,
                                .PayrollIntegration = c.PayrollIntegration,
                                .HumanTalentIntegration = c.HumanTalentIntegration,
                                .DispensingIntegration = c.DispensingIntegration,
                                .CompanyType = c.CompanyType,
                                .CompanyNit = c.CompanyNit,
                                .VerificationDigitNit = c.VerificationDigitNit.Trim,
                                .Address = c.Address,
                                .Telephone = c.Telephone,
                                .ProductionCompany = c.ProductionCompany,
                                .State = c.State,
                                .Version = c.Version,
                                .SecurityContainer = scn,
                                .TenantId = If(tc Is Nothing, CShort(0), tc.TenantId),
                                .TenantName = If(t Is Nothing, "", t.Name),
                                .ServiceConfigurationId = If(c.ServiceConfiguration Is Nothing, CByte(0), c.ServiceConfiguration.Id),
                                .Administrator = True,
                                .RollId = If(r Is Nothing, CInt(0), r.Id),
                                .RollCode = r.RollCode,
                                .RollName = r.Description,
                                .GroupId = If(g Is Nothing, CInt(0), g.Id),
                                .GroupCode = g.Code,
                                .GroupName = g.Description,
                                .Environment = If(c.ProductionCompany, "Producción", "Prueba"),
                                .CountryName = If(co Is Nothing, "", co.Name),
                                .Flagcode = If(co Is Nothing, "", co.Flagcode),
                                .ArchitectureType = If(c.ArchitectureType = 0, CByte(1), c.ArchitectureType),
                                .DecimalSeparator = c.DecimalSeparator,
                                .ServiceConfiguration = sc,
                                .ClientIdGuid = c.ClientId
                                }

            Case UserType.TenantAdmin
                _Containers = From tu In _context.TenantUsers.Where(Function(tu) tu.UserId = idUser AndAlso tu.State)
                              Join tc In _context.TenantContainer On tc.TenantId Equals tu.TenantId
                              Join t In _context.Tenant On t.Id Equals tc.TenantId
                              Join co In _context.Countries On co.Id Equals t.CountryId
                              Join c In _context.Containers.Where(Function(c) c.State) On c.Id Equals tc.ContainerId
                              Join r In _context.Roll On r.Id Equals tu.RollId
                              Join g In _context.Group On g.Id Equals tu.GroupId
                              Join sc In _context.ServiceConfiguration On sc.Id Equals c.ServiceConfigurationId
                              Select New Company() With {
                                .Id = c.Id,
                                .Code = c.Code,
                                .Name = c.Name,
                                .FoundationalContainer = c.FoundationalContainer,
                                .TransactionalContainer = c.TransactionalContainer,
                                .DocumentalContainer = c.DocumentalContainer,
                                .VituelContainer = c.VituelContainer,
                                .HISContainer = c.HISContainer,
                                .InteropCostContainer = c.InteropCostContainer,
                                .HISIntegration = c.HISIntegration,
                                .GlossesIntegration = c.GlossesIntegration,
                                .PayrollIntegration = c.PayrollIntegration,
                                .HumanTalentIntegration = c.HumanTalentIntegration,
                                .DispensingIntegration = c.DispensingIntegration,
                                .CompanyType = c.CompanyType,
                                .CompanyNit = c.CompanyNit,
                                .VerificationDigitNit = c.VerificationDigitNit.Trim,
                                .Address = c.Address,
                                .Telephone = c.Telephone,
                                .ProductionCompany = c.ProductionCompany,
                                .State = c.State,
                                .Version = c.Version,
                                .SecurityContainer = scn,
                                .TenantId = If(tc Is Nothing, CShort(0), tc.TenantId),
                                .TenantName = If(t Is Nothing, "", t.Name),
                                .ServiceConfigurationId = If(c.ServiceConfiguration Is Nothing, CByte(0), c.ServiceConfiguration.Id),
                                .Administrator = True,
                                .RollId = If(r Is Nothing, CInt(0), r.Id),
                                .RollCode = r.RollCode,
                                .RollName = r.Description,
                                .GroupId = If(g Is Nothing, CInt(0), g.Id),
                                .GroupCode = g.Code,
                                .GroupName = g.Description,
                                .Environment = If(c.ProductionCompany, "Producción", "Prueba"),
                                .CountryName = If(co Is Nothing, "", co.Name),
                                .Flagcode = If(co Is Nothing, "", co.Flagcode),
                                .ArchitectureType = If(c.ArchitectureType = 0, CByte(1), c.ArchitectureType),
                                .DecimalSeparator = c.DecimalSeparator,
                                .ServiceConfiguration = sc,
                                .ClientIdGuid = c.ClientId
                                }

            Case UserType.CompanyAdmin, UserType.StandardUser
                _Containers = From pc In _context.PermissionCompany.Where(Function(pc) pc.IdUser = idUser AndAlso pc.Permission)
                              Join c In _context.Containers.Where(Function(c) c.State) On c.Id Equals pc.IdContainer
                              Join tc In _context.TenantContainer On tc.ContainerId Equals c.Id
                              Join t In _context.Tenant On t.Id Equals tc.TenantId
                              Join co In _context.Countries On co.Id Equals t.CountryId
                              Join tu In _context.TenantUsers.Where(Function(tu) tu.UserId = idUser AndAlso tu.State) On tu.TenantId Equals tc.TenantId
                              Join r In _context.Roll On r.Id Equals tu.RollId
                              Join g In _context.Group On g.Id Equals tu.GroupId
                              Join sc In _context.ServiceConfiguration On sc.Id Equals c.ServiceConfigurationId
                              Select New Company() With {
                                .Id = c.Id,
                                .Code = c.Code,
                                .Name = c.Name,
                                .FoundationalContainer = c.FoundationalContainer,
                                .TransactionalContainer = c.TransactionalContainer,
                                .DocumentalContainer = c.DocumentalContainer,
                                .VituelContainer = c.VituelContainer,
                                .HISContainer = c.HISContainer,
                                .InteropCostContainer = c.InteropCostContainer,
                                .HISIntegration = c.HISIntegration,
                                .GlossesIntegration = c.GlossesIntegration,
                                .PayrollIntegration = c.PayrollIntegration,
                                .HumanTalentIntegration = c.HumanTalentIntegration,
                                .DispensingIntegration = c.DispensingIntegration,
                                .CompanyType = c.CompanyType,
                                .CompanyNit = c.CompanyNit,
                                .VerificationDigitNit = c.VerificationDigitNit.Trim,
                                .Address = c.Address,
                                .Telephone = c.Telephone,
                                .ProductionCompany = c.ProductionCompany,
                                .State = c.State,
                                .Version = c.Version,
                                .SecurityContainer = scn,
                                .TenantId = If(tc Is Nothing, CShort(0), tc.TenantId),
                                .TenantName = If(t Is Nothing, "", t.Name),
                                .ServiceConfigurationId = If(c.ServiceConfiguration Is Nothing, CByte(0), c.ServiceConfiguration.Id),
                                .Administrator = If(pc.Administrator Is Nothing, False, pc.Administrator),
                                .RollId = If(r Is Nothing, CInt(0), r.Id),
                                .RollCode = r.RollCode,
                                .RollName = r.Description,
                                .GroupId = If(g Is Nothing, CInt(0), g.Id),
                                .GroupCode = g.Code,
                                .GroupName = g.Description,
                                .Environment = If(c.ProductionCompany, "Producción", "Prueba"),
                                .CountryName = If(co Is Nothing, "", co.Name),
                                .Flagcode = If(co Is Nothing, "", co.Flagcode),
                                .IdOperatingUnitDefault = pc.IdOperatingUnitDefault,
                                .ArchitectureType = If(c.ArchitectureType = 0, CByte(1), c.ArchitectureType),
                                .DecimalSeparator = c.DecimalSeparator,
                                .ServiceConfiguration = sc,
                                .ClientIdGuid = c.ClientId
                                }
            Case Else
        End Select
        If _Containers IsNot Nothing Then
            Return _Containers.ToList
        Else
            Return New List(Of Company)
		End If

	End Function

	Public Function ListCompaniesByContainers(containers As String) As List(Of Company) Implements IContainersRepository.ListCompaniesByContainers
		Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
			Dim query = String.Format("
        SELECT 
	        C.Id, C.Code, C.Name, C.FoundationalContainer, C.TransactionalContainer, C.DocumentalContainer, C.VituelContainer, C.HISContainer, C.InteropCostContainer, C.HISIntegration,
	        C.GlossesIntegration, C.PayrollIntegration, C.HumanTalentIntegration,C.DispensingIntegration, C.CompanyNit, C.Address, C.Telephone, C.ProductionCompany,
	        C.LyncIntegration, C.Version, C.[State], C.CompanyType, C.VerificationDigitNit, C.ServiceConfigurationId, TC.TenantId, C.ArchitectureType, C.DecimalSeparator
        FROM [Security].[Containers] C
        LEFT OUTER JOIN [Security].TenantContainer TC ON TC.ContainerId = C.Id
        WHERE C.[State] = 1 and C.TransactionalContainer in ({0})", containers)

			Dim dt As DataTable = conx.ExecuteCommand_Data(query)
			Dim res As List(Of Company) = New List(Of Company)()
			For Each r As DataRow In dt.Rows
				Dim company = New Company()
				company.Id = CInt(r("Id"))
				company.Code = r("Code").ToString()
				company.Name = r("Name").ToString()
				company.FoundationalContainer = r("FoundationalContainer").ToString()
				company.TransactionalContainer = r("TransactionalContainer").ToString()
				company.DocumentalContainer = r("DocumentalContainer").ToString()
				company.VituelContainer = r("VituelContainer").ToString()
				company.HISContainer = r("HISContainer").ToString()
				company.InteropCostContainer = r("InteropCostContainer").ToString()
				company.HISIntegration = CByte(r("HISIntegration"))
				company.GlossesIntegration = CByte(r("GlossesIntegration"))
				company.PayrollIntegration = CByte(r("PayrollIntegration"))
				company.HumanTalentIntegration = CByte(r("HumanTalentIntegration"))
				company.DispensingIntegration = CByte(r("DispensingIntegration"))
				company.CompanyType = CByte(r("CompanyType"))
				company.CompanyNit = r("CompanyNit").ToString()
				company.VerificationDigitNit = r("VerificationDigitNit").ToString().Trim()
				company.Address = r("Address").ToString()
				company.Telephone = r("Telephone").ToString()
				company.ProductionCompany = CBool(r("ProductionCompany"))
				company.State = CBool(r("State"))
				company.Version = r("Version").ToString()
				company.SecurityContainer = Me.getSecurityContainerName()
				company.TenantId = If(r("TenantId") Is DBNull.Value, CShort(0), CShort(r("TenantId")))
				company.ServiceConfigurationId = If(r("ServiceConfigurationId") Is DBNull.Value, CByte(0), CByte(r("ServiceConfigurationId")))
				company.ArchitectureType = If(r("ArchitectureType") Is DBNull.Value, CByte(1), CByte(r("ArchitectureType")))
				company.DecimalSeparator = r("DecimalSeparator")?.ToString()
				res.Add(company)
			Next
			Return res
		End Using
	End Function

	''' <summary>
	''' Función para obtener una lista de Zonas horarias
	''' </summary>
	''' <returns>Lista de Contenedores</returns>
	Public Function getgetTimezone() As List(Of Timezone) Implements IContainersRepository.getTimezone
        Dim result = (From t As Timezone In _context.Timezone
                      Select t).ToList
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result
        Else
            Return New List(Of Timezone)()
        End If
    End Function

    ''' <summary>
    ''' Función para obtener una lista de Zonas horarias
    ''' </summary>
    ''' <returns>Lista de Contenedores</returns>
    Public Function getTimezoneById(idTimezone As Integer) As Timezone Implements IContainersRepository.getTimezoneById
        Dim result = (From t As Timezone In _context.Timezone Where t.Id = idTimezone
                      Select t).FirstOrDefault
        If result IsNot Nothing Then
            Return result
        Else
            Return New Timezone()
        End If
    End Function

    ''' <summary>
    ''' Consulta el Container por el TransactionalContainer
    ''' </summary>
    ''' <param name="transactionalContainer"></param>
    ''' <returns></returns>
    Public Function getContainersByTransactionalContainer(transactionalContainer As String) As Containers Implements IContainersRepository.getContainersByTransactionalContainer
        Dim result = (From a In _context.Containers
                      Where a.TransactionalContainer = transactionalContainer
                      Select a).FirstOrDefault

        If result IsNot Nothing Then
            Return result
        Else
            Return New Containers
        End If
    End Function
End Class