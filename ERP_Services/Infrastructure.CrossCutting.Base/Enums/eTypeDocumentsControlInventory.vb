''' <summary>
''' Enumeracion del control de documentos de inventarios
''' </summary>
''' <remarks></remarks>
Public Enum eTypeDocumentsControlInventory As Integer
    ''' <summary>
    ''' Orden de compra 
    ''' </summary>
    ''' <remarks></remarks>
    PurchaseOrder = 1
    ''' <summary>
    ''' Remision de Entrada
    ''' </summary>
    ''' <remarks></remarks>
    ReferralInput = 2
    ''' <summary>
    ''' Ajuste de Inventario
    ''' </summary>
    ''' <remarks></remarks>
    Inventoryadjustment = 3
    ''' <summary>
    ''' Solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Requests = 4
    ''' <summary>
    ''' Dispensacion Farmaceutica
    ''' </summary>
    ''' <remarks></remarks>
    Pharmaceuticaldispensing = 5
    ''' <summary>
    ''' Remision de Salida
    ''' </summary>
    ''' <remarks></remarks>
    ReferralOutput = 6
    ''' <summary>
    ''' Devolucion de Remisiones
    ''' </summary>
    ''' <remarks></remarks>
    ReferralReturn = 7
    ''' <summary>
    ''' Comprobante de entrada
    ''' </summary>
    ''' <remarks></remarks>
    VoucherofEntry = 8
    ''' <summary>
    ''' Devolucion de compra 
    ''' </summary>
    ''' <remarks></remarks>
    ReturnPurchase = 9
    ''' <summary>
    ''' Prestamo de mercancia
    ''' </summary>
    ''' <remarks></remarks>
    loanMerchadinsing = 10
    ''' <summary>
    ''' Devolucion de Dispensacion Farmaceutica
    ''' </summary>
    ''' <remarks></remarks>
    ReturnPharmaceuticaldispensing = 11
    ''' <summary>
    ''' Devolucion de prestamo
    ''' </summary>
    ''' <remarks></remarks>
    Devolutionofloan = 12
    ''' <summary>
    ''' Orden de Traslado
    ''' </summary>
    ''' <remarks></remarks>
    TransferOrder = 13
    ''' <summary>
    ''' Devolucion de orden de traslado
    ''' </summary>
    ''' <remarks></remarks>
    TransferOrderDevolution = 14
    ''' <summary>
    ''' Remisión de Inventario en consignación
    ''' </summary>
    ''' <remarks></remarks>
    ConsignmentInventoryRemission = 15
    ''' <summary>
    ''' Remisión de Inventario en consignación
    ''' </summary>
    ''' <remarks></remarks>
    ProductIntransit = 19
End Enum