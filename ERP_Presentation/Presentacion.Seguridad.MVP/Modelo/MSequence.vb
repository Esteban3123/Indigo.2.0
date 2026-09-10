#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Domain.Security.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Modelo de datos del frontal de secuencias
''' </summary>
Public Class MSequence
    Implements IDisposable

#Region "Fields"

    Private _indigo As SessionValues = SessionValues.Instance

    Private _listModules As List(Of VieModule) = Nothing
    Private _listForms As List(Of VieForm) = Nothing

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista los modulos disponibles
    ''' </summary>
    ''' <returns>Lista de modulos</returns>
    Public Function ListModulesAsync() As List(Of VieModule)
        'Dim list = BaseClass.GetXmlWithAggregates(Of VieModule)(eDataXml.XMLModules)
        'Dim list1 = list.Where(Function(m) m.Forms.Any(Function(f) f.HasSequence)).ToList()
        'Return list1
        If _listModules Is Nothing Then
            _listModules = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListModules(_indigo)
        End If

        Return _listModules
    End Function

    ''' <summary>
    ''' Lista los formularios disponibles
    ''' </summary>
    ''' <param name="moduleId">Id del modulo</param>
    ''' <returns>Lista de formularios</returns>
    Public Function ListForms(ByVal moduleId As Integer) As List(Of VieForm)
        'Return BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLForms).Where(Function(f) f.IdModuleSource = moduleId AndAlso f.HasSequence).ToList()
        If _listForms Is Nothing Then
            _listForms = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListForms(_indigo)
        End If

        Return _listForms.Where(Function(f) f.Module.Id = moduleId AndAlso f.HasSequence).ToList()
    End Function

    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns>Lista de unidades operativas</returns>
    Public Async Function ListOperatingUnits() As Threading.Tasks.Task(Of List(Of Domain.Entities.OperatingUnit))
        Dim listOperatingUnit = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllOperatingUnitAsync(_indigo, _indigo.TransactionalContainer)
        Return listOperatingUnit
    End Function

    ''' <summary>
    ''' Lista los patrones de secuencia
    ''' </summary>
    ''' <returns>Lista de secuencias</returns>
    Public Async Function ListSequencePatterns() As Threading.Tasks.Task(Of List(Of Domain.Entities.Sequense))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListSequencesAsync(_indigo)
    End Function

    ''' <summary>
    ''' Obtiene una configuración de secuencia segun el modulo y el frontal
    ''' </summary>
    ''' <param name="idModule">Id del modulo</param>
    ''' <param name="idForm">Id del frontal</param>
    ''' <returns>Configuración de secuencia</returns>
    Public Async Function GetSequence(ByVal sequenceModule As String, ByVal idForm As String) As Threading.Tasks.Task(Of Object)
        Select Case sequenceModule
            Case "MedicalFees" '100 'Cuentas medicas
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.GetSequenseByIdFormAsync(idForm)
            Case "Glosas" 'Glosas
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetSequenseByIdFormAsync(idForm, Me._indigo)
            Case "Payroll" '120 'Nómina
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetSequenseByIdFormAsync(idForm, Me._indigo)
            Case "Accounting" '130 'Contabilidad General
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetSequenseByIdFormAsync(idForm)
            Case "Maintenance" '140 'Mantenimiento
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSequenseByIdFormAsync(idForm, _indigo)
            Case "Payments" '150 'Cuentas por Pagar
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetSequenseByIdFormAsync(idForm)
            Case "Portfolio" '160 'Cuentas por Cobrar
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetSequenseByIdFormAsync(idForm)
            Case "Billing" '180 'Facturacion
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSequenseByIdFormAsync(idForm)
            Case "Inventory" '190 'Inventarios
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetSequenseByIdFormAsync(idForm)
            Case "Budget" '200 'Presupuesto
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetSequenseByIdFormAsync(idForm)
            Case "FixedAssets" '210 'Activos Fijos
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetSequenseByIdFormAsync(idForm)
            Case "Treasury" '220 'Administracion de Efectivo
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSequenseByIdFormAsync(idForm)
            Case "Portfolio" 'Juridico
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetSequenseByIdFormAsync(idForm)
            Case "InteropCost" '250 'Costos
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetSequenseByIdFormAsync(idForm)
            Case "Contract" ''300 'Contratos EAPB
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetSequenseByIdFormAsync(idForm)
            Case "Cost" '510 'Costos
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetSequenseByIdFormAsync(idForm)
            Case "MixingStation" '520 'Central de Mezclas
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetSequenceByIdFormAsync(idForm)
            Case "Authorization" '521 'Autorizaciones
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.GetSequenseByIdFormAsync(idForm)
            Case "Admissions"
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAdmissions.GetSequenseByIdFormAsync(idForm)
            Case "AccountManagement" 'Gestion de cuentas 
                Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetSequenseByIdFormAsync(idForm)
            Case Else
                Return Nothing
        End Select
    End Function

    ''' <summary>
    ''' Obtiene una nueva instancia del objeto de secuencia segun el modulo
    ''' </summary>
    ''' <param name="idModule">Id del modulo</param>
    ''' <param name="idForm">Id del frontal</param>
    ''' <returns>Nueva instancia de secuencia</returns>
    Public Function GetObjSequenceCInstanceByVal(sequenceModule As String, ByVal idForm As String) As Object
        Dim res As Object = Nothing
        Select Case sequenceModule
            Case "MedicalFees" '100 'Cuentas medicas
                res = New Domain.Entities.MedicalFeesSecuence()
            Case "Glosas" 'Glosas
                res = New Domain.Entities.GlosaSequence()
            Case "Payroll" '120 'Nómina
                res = New Domain.Entities.PayrollSequence()
            Case "Accounting" '130 'Contabilidad General
                res = New Domain.Entities.GeneralLedgerSequence()
            Case "Maintenance" '140 'Mantenimiento
                res = New Domain.Entities.MaintenanceSequence()
            Case "Payments" '150 'Cuentas por Pagar
                res = New Domain.Entities.PaymentsSecuence()
            Case "Portfolio" '160 'Cuentas por Cobrar, juridico
                res = New Domain.Entities.PortfolioSequence()
            Case "Billing" '180 'Facturacion
                res = New Domain.Entities.BillingSequence()
            Case "Inventory" '190 'Inventarios
                res = New Domain.Entities.InventorySequence()
            Case "Budget" '200 'Presupuesto
                res = New Domain.Entities.BudgetSequence()
            Case "FixedAssets" '210 'Activos Fijos
                res = New Domain.Entities.FixedAssetSequence()
            Case "Treasury" '220 'Administracion de Efectivo
                res = New Domain.Entities.TreasurySequence()
            Case "InteropCost" '250 'Costos
                res = New Domain.Entities.InteropCostSecuence()
            Case "Contract" '300 'Contratos EAPB
                res = New Domain.Entities.ContractSequence()
            Case "Cost" '510 'Costos
                res = New Domain.Entities.CostSecuence()
            Case "MixingStation" '520 'Central de Mezclas
                res = New Domain.Entities.MixingStationSequence()
            Case "Authorization" '521 'Autorizaciones
                res = New Domain.Entities.AuthorizationSequence()
            Case "Admissions" ' 'Admisiones
                res = New Domain.Entities.AdmissionsSequence()
            Case "AccountManagement" ' 'Gestion de cuentas
                res = New Domain.Entities.AccountManagementSequence()
            Case Else
                res = Nothing
        End Select
        If res IsNot Nothing Then
            res.IdForm = idForm
            res.IsManual = True
            res.Scope = "O"
            res.Sequential = False
            res.Rate = 1
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene una nueva instancia del objeto de secuencia segun el modulo
    ''' </summary>
    ''' <param name="idModule">Id del modulo</param>
    ''' <param name="idForm">Id del frontal</param>
    ''' <returns>Nueva instancia de secuencia</returns>
    Public Function GetObjSequenceDInstance(sequenceModule As String) As Object
        Dim res As Object = Nothing
        Select Case sequenceModule
            Case "MedicalFees" '100 'Cuentas medicas
                res = New Domain.Entities.MedicalFeesSecuenceDetail()
            Case "Glosas" 'Glosas
                res = New Domain.Entities.GlosaSequenceDetail()
            Case "Payroll" '120 'Nómina
                res = New Domain.Entities.PayrollSequenceDetail()
            Case "Accounting" '130 'Contabilidad General
                res = New Domain.Entities.GeneralLedgerSequenceDetail()
            Case "Maintenance" '140 'Mantenimiento
                res = New Domain.Entities.MaintenanceSequenceDetail()
            Case "Payments" '150 'Cuentas por Pagar
                res = New Domain.Entities.PaymentsSecuenceDetail()
            Case "Portfolio" '160 'Cuentas por Cobrar, juridico
                res = New Domain.Entities.PortfolioSequenceDetail()
            Case "Billing" '180 'Facturacion
                res = New Domain.Entities.BillingSequenceDetail()
            Case "Inventory" '190 'Inventarios
                res = New Domain.Entities.InventorySequenceDetail()
            Case "Budget" '200 'Presupuesto
                res = New Domain.Entities.BudgetSequenceDetail()
            Case "FixedAssets" '210 'Activos Fijos
                res = New Domain.Entities.FixedAssetSequenceDetail()
            Case "Treasury" '220 'Administracion de Efectivo
                res = New Domain.Entities.TreasurySequenceDetail()
            Case "InteropCost" '250 'Costos
                res = New Domain.Entities.InteropCostSecuenceDetail()
            Case "Contract" '300 'Contratos EAPB
                res = New Domain.Entities.ContractSequenceDetail()
            Case "Cost" '510 'Costos
                res = New Domain.Entities.CostSecuenceDetail()
            Case "MixingStation" '520 'Central de Mezclas
                res = New Domain.Entities.MixingStationSequenceDetail()
            Case "Authorization" '521 'Autorizaciones
                res = New Domain.Entities.AuthorizationSequenceDetail()
            Case "Admissions" ' 'Admisiones
                res = New Domain.Entities.AdmissionsSequenceDetail()
            Case "AccountManagement" ' 'Gestion de cuentas
                res = New Domain.Entities.AccountManagementSequenceDetail()
            Case Else
                res = Nothing
        End Select
        If res IsNot Nothing Then
            res.Next = 1
        End If
        Return res
    End Function

    ''' <summary>
    ''' Guarda un secuencia numericas
    ''' </summary>
    ''' <param name="idModule">Id del modulo</param>
    ''' <param name="seq">Secuencia a guardar</param>
    ''' <returns>Rsultado de la acción</returns>
    Public Async Function SaveSequence(sequenceModule As String, ByVal seq As Object) As Threading.Tasks.Task(Of ActionResult)
        Dim res As ActionResult = Nothing
        Select Case sequenceModule
            Case "MedicalFees" '100 'Cuentas medicas
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.SaveSequenceAsync(CType(seq, Domain.Entities.MedicalFeesSecuence))
            Case "Glosas" 'Glosas
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveSequenceAsync(CType(seq, Domain.Entities.GlosaSequence), Me._indigo)
            Case "Payroll" '120 'Nómina
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveSequenceAsync(CType(seq, Domain.Entities.PayrollSequence), Me._indigo)
            Case "Accounting" '130 'Contabilidad General
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveSequenceAsync(CType(seq, Domain.Entities.GeneralLedgerSequence))
            Case "Maintenance" '140 'Mantenimiento
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveSequenceAsync(CType(seq, Domain.Entities.MaintenanceSequence), Me._indigo)
            Case "Payments" '150 'Cuentas por Pagar
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveSequenceAsync(CType(seq, Domain.Entities.PaymentsSecuence))
            Case "Portfolio" '160 'Cuentas por Cobrar
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SaveSequenceAsync(CType(seq, Domain.Entities.PortfolioSequence))
            Case "Billing" '180 'Facturacion
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveSequenceAsync(CType(seq, Domain.Entities.BillingSequence))
            Case "Inventory"  '190 'Inventarios
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveSequenceAsync(CType(seq, Domain.Entities.InventorySequence))
            Case "Budget" '200 'Presupuesto
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveSequenceAsync(CType(seq, Domain.Entities.BudgetSequence))
            Case "FixedAssets" '210 'Activos Fijos
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveSequenceAsync(CType(seq, Domain.Entities.FixedAssetSequence))
            Case "Treasury" '220 'Administracion de Efectivo
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveSequenceAsync(CType(seq, Domain.Entities.TreasurySequence))
            Case "InteropCost" '250 'Costos
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveSequenceAsync(CType(seq, Domain.Entities.InteropCostSecuence))
            Case "Contract" '300 'Contratos EAPB
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoContract.SaveSequenceAsync(CType(seq, Domain.Entities.ContractSequence))
            Case "Cost" '510 'Costos
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveSequenceAsync(CType(seq, Domain.Entities.CostSecuence))
            Case "MixingStation" '520 'Central de Mezclas
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveSequenceAsync(CType(seq, Domain.Entities.MixingStationSequence))
            Case "Authorization" '521 'Autorizaciones
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.SaveSequenceAsync(CType(seq, Domain.Entities.AuthorizationSequence))
            Case "Admissions" ' 'Admisiones
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoAdmissions.SaveSequenceAsync(CType(seq, Domain.Entities.AdmissionsSequence))
            Case "AccountManagement" ' 'Gestion de cuentas
                res = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.SaveSequenceAsync(CType(seq, Domain.Entities.AccountManagementSequence))
            Case Else
                res = Nothing

        End Select
        Return res
    End Function

    ''' <summary>
    ''' Guarda un patrón de secuencia numerica
    ''' </summary>
    ''' <param name="seq">Patrón de secuencia a guardar</param>
    ''' <returns>Rsultado de la acción</returns>
    Public Async Function SavePatternSequence(ByVal seq As Domain.Entities.Sequense) As Threading.Tasks.Task(Of ActionResult)
        Return Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SavePatternSequenceAsync(seq, Me._indigo)
    End Function

    ''' <summary>
    ''' Obtiene un patrón de secuencia numerica
    ''' </summary>
    ''' <param name="idSeq">Id del patrón</param>
    ''' <returns>Patrón de secuencia</returns>
    Public Async Function GetPatternSequence(ByVal idSeq As Integer) As Threading.Tasks.Task(Of Domain.Entities.Sequense)
        Return Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetPatternSequenceAsync(idSeq, Me._indigo)
    End Function

    ''' <summary>
    ''' Obtiene un patrón de secuencia numerica
    ''' </summary>
    ''' <param name="idSeq">Id del patrón</param>
    ''' <returns>Patrón de secuencia</returns>
    Public Async Function GetPatternSequenceByName(ByVal nameSeq As String) As Threading.Tasks.Task(Of Domain.Entities.Sequense)
        Return Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetPatternSequenceByNameAsync(nameSeq, Me._indigo)
    End Function

    ''' <summary>
    ''' Lista todos los prefijos registrados
    ''' </summary>
    ''' <param name="isComp">Valor que indica si comprobante de egreso</param>
    ''' <returns>Lista de prefijos</returns>
    Public Async Function ListTreasuryPrefixs(Optional ByVal isComp As Boolean = False) As Threading.Tasks.Task(Of List(Of String))
        If isComp Then
            Dim prefixs As List(Of String) = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListPrefixsCashRegisterAsync()
            If prefixs IsNot Nothing Then
                Dim others As List(Of String) = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListPrefixsEntityBankAccountAsync()
                If others IsNot Nothing Then
                    prefixs.AddRange(others)
                End If
                Return prefixs
            Else
                Return New List(Of String)()
            End If
        Else
            Dim prefixs As List(Of String) = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListPrefixsCashRegisterAsync()
            If prefixs IsNot Nothing Then
                Return prefixs
            Else
                Return New List(Of String)()
            End If
        End If
    End Function

    ''' <summary>
    ''' Lista todos los prefijos registrados
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    Public Async Function ListInventoryPrefixs() As Threading.Tasks.Task(Of List(Of String))
        Dim prefixs As List(Of String) = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListPrefixsAsync()
        If prefixs IsNot Nothing Then
            Return prefixs
        Else
            Return New List(Of String)()
        End If
        Return New List(Of String)()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class