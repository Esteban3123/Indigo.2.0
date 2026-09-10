Public Class ModuleDictionary

    Private _dictionary As New Dictionary(Of String, String)()

    Public Sub New()
        LoadDictionary()
    End Sub

    ''' <summary>
    ''' Metodo para poder cargar el diccionario con todos los Formularios y 
    ''' sus respectivos Modulos para determinar la configuración de la Queue que debe utilizar
    ''' </summary>
    Private Sub LoadDictionary()
        _dictionary.Add("supplier", "Maintenance")
        _dictionary.Add("product", "Inventory")
        _dictionary.Add("costCenter", "Payroll")
        _dictionary.Add("packagingUnit", "Inventory")
        _dictionary.Add("productType", "Inventory")
        _dictionary.Add("medicament", "Inventory")
        _dictionary.Add("inventorySupplie", "Inventory")
        _dictionary.Add("electronicRIPS", "ElectronicRIPS")
        _dictionary.Add("customer", "Glosas")
        _dictionary.Add("thirdParty", "Common")
        _dictionary.Add("cUPSEntity", "Contract")
        _dictionary.Add("healthAdministrator", "Contract")
        _dictionary.Add("iPSService", "Contract")
        _dictionary.Add("mainAccounts", "GeneralLedger")
        _dictionary.Add("administrationRoute", "Inventory")
        _dictionary.Add("aTCEntity", "Inventory")
        _dictionary.Add("dCI", "Inventory")
        _dictionary.Add("inventoryMeasurementUnit", "Inventory")
        _dictionary.Add("inventoryRiskLevel", "Inventory")
        _dictionary.Add("manufacturer", "Inventory")
        _dictionary.Add("pharmaceuticalForm", "Inventory")
        _dictionary.Add("pharmacologicalGroup", "Inventory")
        _dictionary.Add("productGroup", "Inventory")
        _dictionary.Add("productSubGroup", "Inventory")
        _dictionary.Add("functionalUnit", "Payroll")
        _dictionary.Add("warehouse", "Inventory")
        _dictionary.Add("AccountDistributionEventFromUI", "Distribution")
        _dictionary.Add("InvoiceCapitated", "Billing")
        _dictionary.Add("CausationPending", "MedicalFees")
    End Sub

    ''' <summary>
    ''' Metodo para poder consultar el Modulo de un Formulario
    ''' </summary>
    ''' <param name="form"></param>
    ''' <returns></returns>
    Public Function GetModule(ByVal form As String)
        Dim value As String = _dictionary(form)
        If value Is Nothing Then
            Throw New ArgumentNullException("El formulario no fue encontrado en el diccionario.")
        End If
        Return value
    End Function


End Class
