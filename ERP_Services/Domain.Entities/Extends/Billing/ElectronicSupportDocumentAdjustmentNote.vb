Imports System.Runtime.Serialization
'Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Root

Partial Public Class ElectronicSupportDocumentAdjustmentNote
    <DataMember>
    Public Property ElectronicSupportDocumentFullName As String
    <DataMember>
    Public Property ElectronicSupportDocumentDate As Date?
    <DataMember>
    Public Property ElectronicSupportDocumentValue As Decimal?




#Region "CUDs"
    ''' <summary>
    ''' Fecha de emisión
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FecDS As String
        Get
            Return Me.DocumentDate.ToString("yyyy-MM-dd")
        End Get
    End Property

    ''' <summary>
    ''' Hora del documento soporte incluyendo UTC - 05:00
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HorDS As String
        Get
            Return Me.DocumentDate.ToString("HH:mm:ss-05:00")
        End Get
    End Property

    ''' <summary>
    ''' Valor del documento soporte sin Impuestos, con punto decimal, con decimales a dos (2) 
    ''' dígitos truncados, sin separadores de miles, ni símbolo pesos
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValDS As String
        Get
            Return Utils.GetFormatNumberWithTwoDecimals(Me.SubTotalValue)
        End Get
    End Property

    ''' <summary>
    ''' Valor fijo según resolución 000167
    ''' </summary>
    Public ReadOnly Property CodImp As String = "01"

    ''' <summary>
    ''' Valor impuesto 01 - IVA, con punto decimal, con decimales a dos (2) dígitos truncados, sin 
    ''' separadores de miles, ni símbolo pesos. Si no está referenciado el impuesto 01 – IVA este valor 
    ''' se representa con 0.00
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValImp As String
        Get
            Return Utils.GetFormatNumberWithTwoDecimals(Me.TaxValue)
        End Get
    End Property

    ''' <summary>
    ''' Valor total del documento soporte
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValTot As String
        Get
            Return Utils.GetFormatNumberWithTwoDecimals(Me.TotalValue)
        End Get
    End Property

    Public Function getQRDefinition() As String
        If Me.ElectronicSupportDocument.Environment = 1 Then
            Return String.Format("https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey={0}", Me.CUDS)
        Else
            Return String.Format("https://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey={0}", Me.CUDS)
        End If
    End Function

    ''' <summary>
    ''' arma el string para el CUDS
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCUDSCode() As String
        Return Infrastructure.CrossCutting.Root.Utils.Sha384Encode(Me.GetCUDSDefinition())
    End Function

    Public Function GetCUDSDefinition() As String
        Return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                             Me.Code,
                             Me.FecDS,
                             Me.HorDS,
                             Me.ValDS,
                             Me.CodImp,
                             Me.ValImp,
                             Me.ValTot,
                             Me.ElectronicSupportDocument.SupplierNit,
                             Me.ElectronicSupportDocument.IndigoCompanyNit,
                             Me.ElectronicSupportDocument.SoftwarePin,
                             Me.ElectronicSupportDocument.Environment
        )
    End Function
#End Region

#Region "Functions"
    ''' <summary>
    ''' 1 - Documento soporte
    ''' 2 - Nota de Ajuste
    ''' 3 - ApplicationResponse
    ''' </summary>
    ''' <param name="customerThirdParty"></param>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    Public Function GetFileName(customerThirdParty As ThirdParty, documentType As TypeElectronicDocument) As String
        Dim format = String.Empty
        Dim documentTypeName = String.Empty
        Dim nit = customerThirdParty.Person.IdentificationNumber.PadLeft(10, "0")
        Dim ptcode = "095" 'Codigo asignado al proveedor tecnológico (INDIGO es 095)
        Dim year = Me.Year.ToString().Substring(2, 2)
        Dim consecutive = Me.Consecutive.ToString.PadLeft(8, "0")

        format = "{0}{1}{2}{3}{4}.xml"

        If documentType = TypeElectronicDocument.DocumentoSoporte Then
            documentTypeName = "ds"
        ElseIf documentType = TypeElectronicDocument.NotaDeAjuste Then
            documentTypeName = "nas"
        ElseIf documentType = TypeElectronicDocument.ApplicationResponse Then
            documentTypeName = "ars"
        End If

        Return String.Format(format, documentTypeName, nit, ptcode, year, consecutive)

    End Function

    Public Function getTypeCode() As String
        Return Domain.Base.Entities.Enums.ElectronicDocuments.v1_6.DocumentType.AdjustmentNoteSupportDocument
    End Function
#End Region


End Class
