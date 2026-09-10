#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Common
Imports Presentation.Controls

#End Region

Public Class FrmExogenousFormat
    Implements IExogenousFormat

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Common"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    Private _presenter As PExogenousFormat

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.GeneralLedgerSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Variable para controlar el registro bloqueado en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _blockRecord As Domain.Entities.BlockRecordGeneralLedger

    ''' <summary>
    ''' Variable que contiene la entidad de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _exogenousFormat As ExogenousFormat

    ''' <summary>
    ''' representa la entidad de detalle de lineas de distirbucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _exogenousFormatDetail As ExogenousFormatDetail

    ''' <summary>
    ''' Listado del detalle de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listExogenousFormatDetail As List(Of ExogenousFormatDetail)

    ''' <summary>
    ''' Lista de eliminados de detalles de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteExogenousFormatDetail As List(Of ExogenousFormatDetail)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IExogenousFormat.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IExogenousFormat.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As GeneralLedgerSequence Implements IExogenousFormat.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As GeneralLedgerSequence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.GeneralLedgerSequenceDetail In Me._sequense.GeneralLedgerSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IExogenousFormat.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Formato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Format As Integer Implements IExogenousFormat.Format
        Get
            Return INDsleFormat.EditValue
        End Get
        Set(value As Integer)
            INDsleFormat.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Versión
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Version As Integer Implements IExogenousFormat.Version
        Get
            Return INDtxtVersion.Text
        End Get
        Set(value As Integer)
            INDtxtVersion.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Valor mínimo a reportar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MinimumValue As Decimal Implements IExogenousFormat.MinimumValue
        Get
            Return INDtxtMinimumValue.Text
        End Get
        Set(value As Decimal)
            INDtxtMinimumValue.Text = value
        End Set
    End Property

#Region "Details"

    ''' <summary>
    ''' Concepto del detalle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Concept As Integer? Implements IExogenousFormat.Concept
        Get
            Return INDsleConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Columna del concepto del formato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptType As Byte? Implements IExogenousFormat.ConceptType
        Get
            Return INDsleConceptType.EditValue
        End Get
        Set(value As Byte?)
            INDsleConceptType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountId As Integer? Implements IExogenousFormat.MainAccountId
        Get
            Return INDsleMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Naturaleza a usar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Nature As Byte? Implements IExogenousFormat.Nature
        Get
            Return INDsleNature.EditValue
        End Get
        Set(value As Byte?)
            INDsleNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si el tercero se obtiene:
    ''' 1 - Del documento contable
    ''' 2 - Del documento origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyBy As Byte? Implements IExogenousFormat.ThirdPartyBy
        Get
            Return INDsleThirdPartyBy.EditValue
        End Get
        Set(value As Byte?)
            INDsleThirdPartyBy.EditValue = value
        End Set
    End Property

#End Region

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Establece los formatos
    ''' </summary>
    ''' <remarks></remarks>
    Private _ListFormat As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property ListFormat As List(Of Tuple(Of Integer, String))
        Get
            If _ListFormat Is Nothing Then
                _ListFormat = New List(Of Tuple(Of Integer, String))
                _ListFormat.Add(New Tuple(Of Integer, String)(1001, "1001 - Pagos o abonos en cuenta y retenciones practicadas"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1003, "1003 - Retenciones en la fuente que le practicaron"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1004, "1004 - Descuentos Tributarios"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1005, "1005 - Impuesto a las ventas por pagar - Descontable"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1006, "1006 - Impuesto a las ventas por pagar - Generado"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1007, "1007 - Ingresos recibidos"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1008, "1008 - Saldo de cuentas por cobrar"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1009, "1009 - Saldo de cuentas por pagar"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1010, "1010 - Información de socios,  accionistas, comuneros y/o cooperados"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1011, "1011 - Información de las declaraciones Tributarias"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1012, "1012 - Información de declaraciones tributarias, acciones, inversiones en bonos títulos valores y cuentas de ahorro y cuentas corrientes"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1056, "1056 - Pagos o Abonos en cuenta y retenciones por secretarios generales que administran recursos del tesoro"))
                _ListFormat.Add(New Tuple(Of Integer, String)(1647, "1647 - Ingresos recibidos para terceros"))
                _ListFormat.Add(New Tuple(Of Integer, String)(2275, "2275 - Ingresos No Constitutivos de Renta Ni Ganancia Ocasional"))
                _ListFormat.Add(New Tuple(Of Integer, String)(2276, "2276 - Información de rentas de trabajo y pensiones"))
            End If
            Return _ListFormat
        End Get
    End Property

    ''' <summary>
    ''' Listado de cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IExogenousFormat.MainAccountXpo
        Get
            Return CType(INDsleMainAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleMainAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece los conceptos por formato
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingConcept As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingConcept As List(Of Tuple(Of Integer, String))
        Get
            If _FillingConcept Is Nothing Then
                _FillingConcept = New List(Of Tuple(Of Integer, String))
                If Format = 1001 OrElse Format = 1056 Then
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5002, "5002 - Honorarios de renta NO laboral"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5003, "5003 - Comisiones de renta NO laboral"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5004, "5004 - Servicios de renta NO laboral"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5005, "5005 - Arrendamientos"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5006, "5006 - Intereses y Rendimientos Financieros"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5007, "5007 - Compra de activos movibles (E.T. Art 60)"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5008, "5008 - Compra de activos fijos (E.T. Art 60)"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5010, "5010 - Pago o abonos en cuenta por concepto de aportes parafiscales al SENA, a las Cajas de Compensación Familiar y al Instituto Colombiano de Bienestar Familiar"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5011, "5011 - Pago o abonos en cuenta efectuado a las empresas promotoras de salud EPS y los aportes al Sistema de Riesgos Laborales, incluidos los aportes del trabajador"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5012, "5012 - Pagos o abonos en cuenta por copncepto de aportes obligatorios para pensiones efectuados a los Fondos de Pensiones, incluidos los aportes del trabajador"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5013, "5013 - Donaciones en dinero efectuadas a las entidades señaladas en los artículos 125, 125-4, 126-2 y 158-1 del Estatuto Tributario y la establecida en el artículo 16 de la Ley 814 de 2003, y demás que determine la ley"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5014, "5014 - Donaciones en activos diferentes a dinero efectuadas a las entidades señaladas en los artículos 125, 125-4, 126-2 y 158-1 del Estatuto Tributario y la establecida en el artículo 16 de la Ley 814 de 2003, y demás que determine la ley"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5015, "5015 - Impuestos solicitados como deducción"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5016, "5016 - Demas Costos y Deducciones"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5018, "5018 - Importe de primas de reaseguros pagados o abonados en cuenta"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5019, "5019 - Amortizaciones realizadas durante el año"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5020, "5020 - Compra de activos fijos reales productivos sobre los cuales solicitó deducción, art. 158-3 E.T. El valor acumulado pagado o abonado en cuenta"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5023, "5023 - Valor acumulado de los pagos o abonos en cuenta al exterior por asistencia técnica"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5024, "5024 - Valor acumulado de los pagos o abonos en cuenta al exterior por marcas"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5025, "5025 - Valor acumulado de los pagos o abonos en cuenta al exterior por patentes"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5026, "5026 - Valor acumulado de los pagos o abonos en cuenta al exterior por regalías"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5027, "5027 - Valor acumulado de los pagos o abonos en cuenta al exterior por servicios técnicos"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5028, "5028 - Devolución de pagos o abonos en cuenta y retenciones correspondientes a operaciones de añios anteriores"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5029, "5029 - Gastos pagados por anticipado por Compras"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5030, "5030 - Gastos pagados por anticipado por Honorarios de Renta no laboral"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5031, "5031 - Gastos pagados por anticipado por Comisiones de Renta no laboral"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5032, "5032 - Gastos pagados por anticipado por Servicios de Renta no laboral"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5033, "5033 - Gastos pagados por anticipado por Arrendamientos"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5034, "5034 - Gastos pagados por anticipado por Intereses y Rendimientos Financieros"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5035, "5035 - Gastos pagados por anticipado por otros conceptos"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5043, "5043 - Valor de las Participaciones o dividendos pagados o abonados en cuenta en calidad de exigibles"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5044, "5044 - El pago por loterías, rifas, apuestas y similares"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5045, "5045 - Retención sobre ingresos de tarjetas débito y crédito"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5046, "5046 - Enajenación de activos fijos de personas naturales ante oficinas de tránsito y otras entidades autorizadas"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5047, "5047 - Importe siniestros por lucro cesante pagados o abonados en cuenta"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5048, "5048 - Importe siniestros por daño emergente pagados o abonados en cuenta"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5053, "5053 - Retenciones practicadas a titulo de timbre"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5054, "5054 - La devolución de retenciones a título del impuesto de timbre, correspondientes a operaciones de años anteriores"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5055, "5055 - Viaticos"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5056, "5056 - Gastos de representación"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5058, "5058 - Valor de los aportes, tasas y contribuciones solicitado como deducción"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5059, "5059 - El pago o abono en cuenta realizado a cada uno de los cooperados, del valor del Fondo para revalorización de aportes"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5060, "5060 - Redención de inversiones en lo que corresponde al reembolso del capital po titulo de capitalización"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5061, "5061 - Utilidades pagadas o abonadas en cuenta, cuando el beneficiario es diferente al fideicomitente"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5063, "5063 - Intereses y rendimientos financieros pagados"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5064, "5064 - Devoluciones de saldos de aportes pensionales pagados"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5065, "5065 - Excedentes pensionales de libre disponibilidad componente del capital pagados"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5066, "5066 - El valo del impuesto al consumo solicitado como deducción"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(5067, "5067 - El valor acumulado de los pagos o abonos en cuenta al exterior por consultoría"))
                ElseIf Format = 1003 Then
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1301, "1301 - Retenciones por salarios, prestaciones y demas pagos laborales"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1302, "1302 - Retenciones por ventas"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1303, "1303 - Retenciones por servicios"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1304, "1304 - Retenciones por honorarios"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1305, "1305 - Retenciones por comisiones"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1306, "1306 - Retenciones por intereses y rendimientos financieros"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1307, "1307 - Retenciones por arrendamientos"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1308, "1308 - Otras retenciones por otros conceptos"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1309, "1309 - Retención en la fuente en el Impuesto a las ventas"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1310, "1310 - Retención por dividentos y participaciones"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1311, "1311 - Retención por enajenación de activos de personas naturales ante oficinas de tránsito y otras entidades autorizadas"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1312, "1312 - Retención por ingresos de tarjetas débito y crédito"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1313, "1313 - Retención por loterías, rifas, apuestas y similares"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1314, "1314 - Retención por Impuesto de Timbre"))
                ElseIf Format = 1004 Then
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8302, "8302 - Descuento tributario por impuesto sobre las ventas pagado en la adquisición e importación de maquinaria pesada para industrias básicas. E.T., art. 258-2."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8303, "8303 - Descuento tributario por impuestos pagados en el exterior solicitado como descuento por los contribuyentes nacionales que perciban rentas de fuente extranjera. E.T., art. 254."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8305, "8305 - Descuento tributario empresas de servicios públicos domiciliarios que presten servicios de acueducto y alcantarillado. L. 788/2002, art. 104."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8307, "8307 - Descuento tributario inversión acciones sociedades agropecuarias. E.T., art.249"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8308, "8308 - Descuento tributario de aportes parafiscales y demás, por vinculación de nuevos empleados menores de veintiocho años. L. 1429/10, art. 9."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8309, "8309 - Descuento tributario de aportes parafiscales y demás, por vinculación de personas en situación de desplazamiento, reintegración o discapacidad. L.1429/10, art. 10."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8310, "8310 - Descuento tributario de aportes parafiscales y demás, por vinculación de mujeres mayores de cuarenta (40) años. L. 1429/10, art. 11. "))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8311, "8311 - Descuento tributario de aportes parafiscales y demás, por nuevos empleados que devenguen menos de 1,5 SMMLV. L. 1429/10, art. 13."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8316, "8316 - Descuento tributario por donaciones dirigida a programas de becas o créditos condonables. E.T., art. 158-1, inciso 2° y 256, modificado L.1819/2016, art.91 y 104."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8317, "8317 - Descuento tributario por inversiones en investigación, desarrollo tecnológico e innovación. E.T., art. 158-1 y 256, modificado L. 1819/2016, art. 91 y 104"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8318, "8318 - Descuento por donaciones efectuadas a entidades sin ánimo de lucro pertenecientes al régimen tributario especial. E.T., art. 257, creado L.1819/2016, art. 105."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8319, "8319 - Descuento tributario por donaciones efectuadas a entidades sin ánimo de lucro no contribuyentes de que tratan los artículos 22 y 23 dl estatuto tributario. E.T., art. 257, creado L. 1819/2016, art. 105."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8320, "8320 - Descuento tributario para inversiones realizadas en control, conservación y mejoramiento del medio ambiente. E.T., art. 255, creado L. 1819/2016, art. 103."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8321, "8321 - Descuento tributario por donaciones en la red nacional de bibliotecas públicas y biblioteca nacional E.T., art. 257, parágrafo, creado L. 1819/2016, art. 105."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8322, "8322 - Descuento tributario por donaciones a favor de fondo para reparación de víctimas. Art 177 ley 1498 de 2011 y art. 2.2.10.6. DUR 1084 de 2016."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8323, "8323 - Descuento tributario por impuestos pagados en el exterior por la Entidad controlada del Exterior (ECE). E.T., art.892, adicionado L 1819/2016, art. 139."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8324, "8324 - Descuento tributario por donación a la Corporación General Gustavo Matamoros D’ Costa y demás fundaciones dedicadas a la defensa, protección de derechos humanos. E.T., art 126-2, inciso 1"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8325, "8325 - Descuento tributario por donación a organismos de deporte aficionado. E.T., art. 126- 2, inciso.2."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8326, "8326 - Descuento tributario por donación a organismos deportivos y recreativos o culturales personas jurídicas sin ánimo de lucro, E.T., art. 126-2, inciso 3."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8327, "8327 - Descuento tributario por donaciones efectuadas para el apadrinamiento de parques naturales y conservación de bosques naturales. E.T., art. 126-5."))
                ElseIf Format = 1007 Then
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4001, "4001 - Ingresos brutos de actividades ordinarias"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4002, "4002 - Otros ingresos brutos"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4003, "4003 - Ingresos por intereses y rendimientos financieros."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4004, "4004 - Ingresos por intereses correspondientes a créditos hipotecarios"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4005, "4005 - Ingresos a través de consorcio o uniones temporales"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4006, "4006 - Ingresos a través de mandato o administración delegada"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4007, "4007 - Ingresos a través de exploración y explotación de hidrocarburos, gases y minerales"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4008, "4008 - Ingresos a través de fiducia"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4009, "4009 - Ingresos a través de terceros (Beneficiario)"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4011, "4011 - Ingresos a través de joint venture"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4012, "4012 - Ingresos a través de cuentas en participación"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4013, "4013 - Ingresos a través de convenios de cooperación con entidades públicas"))
                ElseIf Format = 1008 Then
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1315, "1315 - Cuentas por cobrar - Clientes"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1316, "1316 - Cuentas por cobrar - Compañías accionistas, socios y compañías vinculadas"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1317, "1317 - Otras cuentas por cobrar"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1318, "1318 - Saldo fiscal provisión de cartera"))
                ElseIf Format = 1009 Then
                    _FillingConcept.Add(New Tuple(Of Integer, String)(2201, "2201 - Pasivo con proveedores"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(2202, "2202 - Cuentas por pagar a casa matriz, compañías vinculadas, socios y accionistas"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(2203, "2203 - Obligaciones con el sector financiero"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(2204, "2204 - Pasivos por impuestos, gravámenes y tasas"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(2205, "2205 - Pasivos laborales"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(2206, "2206 - Otros pasivos"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(2207, "2207 - Saldo del pasivo por el cálculo actuarial"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(2208, "2208 - Pasivos respaldados en dodumento de fecha cierta"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(2209, "2209 - Pasivos exclusivos de las compañías de seguros"))
                ElseIf Format = 1011 Then
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8104, "8104 - Rentas exentas por venta de energía eléctrica generada con recursos eólicos, biomasa, residuos agrícolas, solar, geotérmica o de los mares. E.T., art. 235-2, num. 7, (Hasta el 2017, E.T. art. 207-2, num. 1). (Derogado. L. 1819/2016, art. 376.num. 2)"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8106, "8106 - Rentas exentas por el aprovechamiento de nuevas plantaciones forestales. E.T. 235-2, num. 4."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8109, "8109 - Rentas exentas por la prestación del servicio de transporte fluvial con embarcaciones y planchones de bajo calado. E.T., art. 235-2, num. 8 (Hasta el 2017 E.T., art. 207-2, num. 2). (E.T. 207-2, Par. 1)"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8111, "8111 - Rentas exentas por la utilidad en la enajenación de predios destinados a fines de utilidad pública. Num 9 Art. 207-2 E.T. Sentencia C-083 del 2018."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8120, "8120 - Rentas exentas por aplicación de algún convenio para evitar la doble tributación."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8121, "8121 - Rentas exentas por derechos de autor por libros de carácter científico y cultural. L. 98/1993, art. 28)."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8125, "8125 - Rentas exentas por Intereses, comisiones y pagos por deuda pública externa, E.T. art. 218 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8127, "8127 - Rentas exentas por inversión en reforestación, aserríos y árboles maderables. E.T., art. 235-2, num. 4. (Hasta el 2017, E.T., art. 207-2, num. 6)."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8140, "8140 - Rentas exentas por aportes voluntarios a los fondos de pensiones. E.T. art. 126-1, inc. 2 "))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8141, "8141 - Rentas exentas por los ahorros a largo plazo para el fomento de la construcción. E.T., art. 126-4."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8142, "8142 - Rentas exentas del beneficio neto para las entidades sin ánimo de lucro. E.T., art 358 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8145, "8145 - Rentas exentas de fondos provenientes de auxilios o donaciones de entidades o gobiernos extranjeros. L. 788/2002. E.T., art. 235-2, num. 5)"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8156, "8156 - Rentas exentas prestaciones provenientes de un fondo de pensiones. E.T. art. 207."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8159, "8159 - Renta exenta pago principal y demás rendimientos generados en actividades financieras por parte de entidades gubernamentales de carácter financiero y de cooperación para el desarrollo. E.T., art. 207-2, num. 12."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8160, "8160 - Rentas exentas de los industriales de la cinematografía, personas naturales. L. 397/1997, art. 46. "))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8161, "8161 - Rentas exentas por indemnizaciones por seguros de vida. E.T. art. 223."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8164, "8164 - Renta exenta por la utilidad en la enajenación de predios destinados al desarrollo de proyectos de vivienda interés social y/o prioritario. E.T., art. 235-2, num. 6, literal a)."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8165, "8165 - Renta exenta por la utilidad en la primera enajenación de viviendas de interés social y/o prioritario. E.T., art. 235-2, num. 6, literal b)."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8166, "8166 - Renta exenta por la utilidad en la enajenación de predios para desarrollo de proyectos de renovación urbana asociados a vivienda de interés social y prioritario. E.T., art. 235-2, num. 6, literal c)."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8167, "8167 - Renta exenta de que trata L. 546/1999, art. 16. Modificado. L. 964/2005 asociados a proyectos de vivienda de interés y prioritario. E.T., art. 235-2, num. 6, literal d)."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8168, "8168 - Renta exenta por rendimientos financieros provenientes de créditos para adquisición de vivienda de interés social y/o prioritario. E.T., art. 235-2, num. 6, literal e)"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8200, "8200 - Deducción en la declaración de renta por las inversiones realizadas en activos fijos reales productivos. E.T., Art. 158-3."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8205, "8205 - Deducción por deterioro de cartera de dudoso o difícil cobro. E.T., art. 145."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8207, "8207 - Costo o deducción por salarios, prestaciones sociales y demás pagos laborales. No debe contener los valores especificados en otros conceptos."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8208, "8208 - Deducción por pagos efectuados a la casa matriz. E.T., art. 124."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8209, "8209 - Deducción por gastos en el exterior. E.T., art. 121. No debe contener los valores especificados en el concepto 8282."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8210, "8210 - Costo en la enajenación de activos fijos poseídos por menos de dos años. E.T., art. 179."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8211, "8211 - Deducción del gravamen a los movimientos financieros. E.T., art. 115."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8212, "8212 - Deducción por agotamiento en explotación de hidrocarburos, E.T., art. 161."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8215, "8215 - Deducción por intereses préstamos vivienda. E.T., art. 119."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8217, "8217 - Deducción por donación o inversión en producción cinematográfica. L. 814/2003, art. 16."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8218, "8218 - Deducción por protección, mantenimiento y conservación muebles e inmuebles de interés cultural, L. 1185/2008, art. 14."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8225, "8225 - Deducción por donaciones del sector privado en la red nacional de bibliotecas públicas y biblioteca nacional. E.T., art 125. Modificado. L. 1819/2016, art. 75."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8227, "8227 - Deducción por concepto de regalías en el país."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8228, "8228 - Costo o deducción por las reparaciones locativas realizadas sobre inmuebles."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8229, "8229 - Deducción por donaciones e inversiones realizadas en investigación, desarrollo tecnológico e innovación. E.T., art. 158-1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8230, "8230 - Deducción por las inversiones realizadas en librerías. L. 98/1993, art. 30."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8231, "8231 - Deducción por la inversión realizada en centros de reclusión. L.633/2000, art.98"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8233, "8233 - Deducción de impuestos devengados, E.T., art. 115. No debe contener los valores especificados en otros conceptos."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8234, "8234 - Costo o deducción de intereses. E.T., art. 117. No debe contener los valores especificados en el concepto 8236."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8235, "8235 - Deducción por las contribuciones a carteras colectivas."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8236, "8236 - Costo o deducción por contratos de leasing. E.T., art. 127-1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8237, "8237 - Costo o deducción por concepto de publicidad y propaganda."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8238, "8238 - Deducción de la provisión de cartera de créditos y provisión de coeficiente de riesgo, provisiones realizadas durante el respectivo año gravable sobre bienes recibidos en dación en pago y sobre contratos de leasing. E.T., art.145, par.1. (Modificado. L. 1819/2016, art. 87)."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8239, "8239 - Deducción por deudas manifiestamente pérdidas o sin valor. E.T., art. 146."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8240, "8240 - Deducción por pérdida de activos. E.T. art. 148."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8241, "8241 - Costo o deducción por aportes al Instituto Colombiano de Bienestar Familiar, (ICBF). E.T., art. 114."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8242, "8242 - Costo o deducción por aportes a Cajas de Compensación Familiar. E.T., art. 114."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8243, "8243 - Costo o deducción por aportes al Servicio Nacional de Aprendizaje, (SENA). E.T., art. 114."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8244, "8244 - Deducción de contribuciones a fondos de pensiones de jubilación e invalidez y fondos de cesantías. E.T. Art. 126-1, modificado L. 1819/2016, art. 15."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8245, "8245 - Deducción por concepto de cesantías pagadas, E.T., art. 109. No debe contener los valores especificados en los conceptos 8248, 8250, 8263 y 8271."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8246, "8246 - Deducción por concepto de aportes a cesantías por los trabajadores independientes. E.T., art- 126-1, inciso 6."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8247, "8247 - Deducción por concepto de contribuciones parafiscales agropecuarias efectuadas por los productores a los fondos de estabilización de la L. 101/1993."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8248, "8248 - Deducción por salarios, prestaciones sociales y demás pagos laborales, pagados a viudas y huérfanos de miembros de las Fuerzas Armadas muertos en combate, secuestrados o desaparecidos. Art. 108-1 del E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8249, "8249 - Costo o deducción por apoyo de sostenimiento mensual de los trabajadores contratados como aprendices. L.115/1994, art. 189."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8250, "8250 - Costo o deducción por salarios pagados, durante el cautiverio, a sus empleados víctimas de secuestros. L. 986/2005, art. 21."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8255, "8255 - Costo o deducción por pagos a terceros por concepto de alimentación del trabajador y su familia o suministro de alimentación para los mismos."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8256, "8256 - Costo o deducción por el pago de estudios a trabajadores en instituciones de educación superior. L. 30/1992, art. 124"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8257, "8257 - Deducción por factor especial de agotamiento en explotación de hidrocarburos, E.T. art. 166."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8259, "8259 - Deducción por tasas y contribuciones fiscales pagadas."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8260, "8260 - Deducción por impuestos, regalías y contribuciones pagados por organismos descentralizados. E.T., art. 116."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8261, "8261 - Deducción de la provisión para el pago de futuras pensiones. E.T., art. 112."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8263, "8263 - Costo o deducción por salarios y prestaciones sociales a trabajadores con discapacidad no inferior al 25%. L.361/1997, art. 31."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8264, "8264 - Deducción por las inversiones realizadas para el transporte aéreo en zonas apartadas del país. L. 633/2000, art. 97."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8265, "8265 - Deducción por aumento en la reserva técnica de FOGAFIN Y FOGACOOP. E.T., art. 19-3."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8267, "8267 - Deducción por contribuciones a fondos mutuos de inversión. E.T., art. 126."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8271, "8271 - Deducción por salarios y prestaciones sociales pagados a mujeres víctimas de violencia comprobada. L. 1257/2008, art. 23."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8272, "8272 - Deducción del 100% por inversiones en infraestructura para la realización de espectáculos públicos. Art. 4 L. 1493/11."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8273, "8273 - Deducción por inversiones en jardines botánicos. L. 299/1996, art. 12."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8274, "8274 - Deducción por inversiones en fuentes de energía no convencional. L.1715/2014, art. 11."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8275, "8275 - Deducción por depreciación de maquinarias, equipos y obras civiles de proyectos de fuentes de energía no convencionales. Art. 14 L. 1715/2014"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8276, "8276 - Costos y deducciones fiscales no reconocidas contablemente (diferencias temporarias), E.T., art. 59 y 105, num.1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8277, "8277 - Deducciones por atenciones a clientes, proveedores y trabajadores. E.T., art. 107-1, inciso 1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8278, "8278 - Deducciones por pagos salariales y prestacionales, provenientes de litigios. E.T., art. 107-1, inciso 2."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8279, "8279 - Deducción de cesantías consolidadas. E.T., art.110. No debe contener los valores especificados en otros conceptos."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8281, "8281 - Deducción especial de Impuesto sobre las ventas por adquisición o importación de bienes de capital gravados a la tarifa general. E.T., art. 115-2 del E.T., adicionado por L. 1819/2016, art. 67."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8282, "8282 - Pagos a jurisdicciones no cooperantes, de baja o nula imposición y a entidades con regímenes preferentes. E.T., art. 124-2"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8283, "8283 - Deducción por depreciación. E.T., art. 128."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8284, "8284 - Costo por depreciación"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8285, "8285 - Costo o deducción por obsolescencia. E.T., art. 129"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8286, "8286 - Deducción de inversiones. E.T., art. 142"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8287, "8287 - Deducción por amortización de activos intangibles. E.T., Art. 143 del E.T. (Modificado por el artículo 85 de la Ley 1819 de 2016)"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8288, "8288 - Amortización inversiones en exploración, desarrollo y construcción de minas, y yacimientos de petróleo y gas. E.T., art. 143-1, modificado L. 1819/2016, art. 86"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8290, "8290 - Pérdidas sufridas en actividades agropecuarias. E.T., art. 150"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8291, "8291 - Deducción por donaciones dirigidas a programas de becas o créditos condonables, E.T., art. 158-1, inciso 2."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8292, "8292 - Deducción por inversiones en evaluación y exploración de recursos naturales no renovables. E.T., Art.159, modificado L. 1819/2016, art. 92."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8293, "8293 - Deducción por agotamiento en explotación de minas, gases distintos de hidrocarburos y depósitos naturales. E.T., art. 167"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8294, "8294 - Deducción por pago impuesto al carbono, como mayor valor del costo del bien. L. 1819/2016, Art. 222, parágrafo 2. Sujeto pasivo del impuesto"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8295, "8295 - Deducción por pagos efectuados a las empresas promotoras de salud EPS y los aportes al Sistema de Riesgos Laborales."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9001, "9001 - Exclusión de IVA por venta de materias primas químicas con destinación específica. E.T., art. 424, num. 1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9002, "9002 - Exclusión de IVA por venta de materias primas destinadas a la producción de vacunas. Num 2 Art. 424 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9003, "9003 - Exclusión de IVA por venta de computadores personales. E.T., art. 424, num. 3, modificado L.1819/2016, art.175, num. 5."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9004, "9004 - Exclusión de IVA por venta de anticonceptivos femeninos. E.T., art.424, num.4."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9007, "9007 - Exclusión de IVA por venta de equipos, entre otros, para construcción, instalación, montaje y operación de sistemas de control y monitoreo ambiental. E.T., art. 424, num. 7."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9008, "9008 - Exclusión de IVA por venta de dispositivos móviles inteligentes. E.T., art. 424, num. 9, modificado L. 1819/2016, art.175, num. 6."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9009, "9009 - Exclusión de IVA por donaciones de alimentos de consumo humano a Bancos de Alimentos. E.T., art. 424, num. 10, modificado L. 1819/2016, art. 175, num. 9."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9010, "9010 - Exclusión de IVA por venta de vehículos pasajeros y sólo reposición. E.T., art. 424, num. 11, modificado L. 1819/2016, art. 175, num. 10"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9012, "9012 - Exclusiones de IVA por venta de objetos con interés artístico, cultural e histórico. E.T., art. 424, num. 13, modificado L. 1819/2016, art. 175, num. 11"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9013, "9013 - Exclusiones de IVA por venta de combustible para aviación para el servicio de transporte aéreo nacional con origen y destino a Guainía, Amazonas, Vaupés, San Andrés Islas y Providencia, Arauca y Vichada. E.T., art. 424, parágrafo 2, modificado L. 1819/2016, art. 175, num. 14."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9014, "9014 - Exclusiones de IVA en la venta de pólizas de seguros de carácter individual. E.T., art. 427. E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9015, "9015 - Exclusión de IVA en equipos, entre otros, para fuentes de energía no convencionales. Art. 12 Ley 1715 de 2014."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9016, "9016 - Exclusión de IVA por venta de servicios médicos odontológicos, entre otros. E.T., art. 476, num. 1. Art. 476 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9017, "9017 - Exclusión de IVA por venta de servicios de transporte. E.T., art. 476, num. 2."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9018, "9018 - Exclusión en IVA en intereses y rendimientos financieros por operaciones de crédito, comisiones de sociedades fiduciarias, fondos comunes. E.T., art. 476, num. 3."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9019, "9019 - Exclusión de IVA en venta de servicios públicos. E.T., art. 476, num.4."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9020, "9020 - Exclusión de IVA en venta de servicio de arrendamiento. E.T., art. 476, num. 5."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9021, "9021 - Exclusión de IVA en venta de servicios de educación. E.T., art. 476, num.6"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9022, "9022 - Exclusión de IVA en venta de servicios de corretaje de reaseguros. E.T., art. 476, num. 7."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9023, "9023 - Exclusión de IVA en venta de planes obligatorios de salud, ahorro individual, riesgos laborales y servicios de seguros y reaseguros. E.T., art. 476, num. 8."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9024, "9024 - Exclusión de IVA en comercialización de animales vivos y servicio de faenamiento. E.T., art. 476, num. 9"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9025, "9025 - Exclusión de IVA en servicios de promoción y fomento deportivo. E.T., art. 476, num. 10."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9026, "9026 - Exclusión de IVA en cine, en eventos y espectáculos. E.T., art. 476, num. 11"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9027, "9027 - Exclusión de IVA en venta de servicios de adecuación de tierras, producción agropecuaria y pesquera. E.T., art. 476, num. 12"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9028, "9028 - Exclusión de IVA comisiones pagadas en procesos de titularización de activos. E.T., art. 476, num. 13"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9029, "9029 - Exclusión de IVA en servicios funerarios, cremación, inhumación y exhumación E.T., art. 476, num. 14."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9030, "9030 - Exclusión de IVA en servicios de conexión y acceso a Internet estrato 3. E.T., art. 476, num."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9031, "9031 - Exclusión de IVA en comisiones por intermediación por la colocación de los planes de salud del sistema general de seguridad social. E.T., art. 476, num. 16"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9032, "9032 - Exclusión de IVA en comisiones percibidas por utilización de tarjetas crédito y débito. E.T., art. 476, num. 17"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9033, "9033 - Exclusión de IVA en servicios de alimentación contratados con recursos públicos y destinados al sistema penitenciario, de asistencia social y escuelas de educación pública. E.T., art. 476, num. 19."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9034, "9034 - Exclusión de IVA en servicios de transporte aéreo nacional donde no exista transporte terrestre organizado. E.T., art. 476, num. 20."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9035, "9035 - Exclusión de IVA en publicidad a través de periódicos y medios regionales. E.T., art. 476, num. 21"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9036, "9036 - Exclusión de IVA a los productos de soporte nutricional del régimen especial. E.T., art. 424, num. 3, adicionado por L. 1819/2016 art. 175."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9037, "9037 - Exclusión de IVA a los alimentos para propósitos médicos especiales para pacientes que requieren nutrición enteral. E.T., art. 424, num. 3, adicionado por L. 1819/2016 art. 175."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9038, "9038 - Exclusión de IVA en el territorio intendencia de San Andrés y Providencia. E.T., art. 423."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9039, "9039 - Exclusión de IVA en los alimentos de consumo humano y animales importados de países colindantes de Vichada, Guajira, Guainía y Vaupés, destinados a consumo local en el departamento. E.T., art. 424, num. 8, modificado L. 1819/2016, art. 175, num. 8"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9040, "9040 - Exclusión de IVA en alimentos, vestuario, elementos de aseo, medicamentos, bicicletas, motocicletas, motocarros y sus partes destinados a los departamentos de Amazonas, Guainía y Vaupés. E.T., art. 424, num. 13, adicionado L 1819/2016, art. 175, num. 13."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9041, "9041 - Exclusión de IVA en compraventa de maquinaria y equipos registrados en el registro nacional de reducción de emisiones de gases efecto invernadero. E.T., art. 424, num. 16, adicionado L. 1819/2016, art. 175, num. 16."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9042, "9042 - Exclusión de IVA en el petróleo crudo recibido por la Agencia Nacional de Hidrocarburos por concepto de regalías para su respectiva monetización. E.T., art. 424, parágrafo, adicionado L. 1819/2016, art. 175."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9043, "9043 - Exclusión de IVA en los servicios de educación virtual para el desarrollo de contenidos digitales. E.T., art. 476, num. 23, adicionado por L. 1819/2016 art. 187."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9044, "9044 - Exclusión de IVA en suministro de páginas web, servidores, computadora en la nube y mantenimiento a distancia. E.T., art. 476, num. 24, adicionado por L. 1819/2016 art. 187."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9045, "9045 - Exclusión de IVA en adquisición de licencias de software para el desarrollo comercial de contenidos digitales. E.T., art. 476, num. 25, adicionado por L. 1819/2016 art. 187."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9046, "9046 - Exclusión de IVA en servicios de reparación y mantenimiento de naves y artefactos marítimos y fluviales. E.T., art. 476, num. 26, adicionado por L. 1819/2016 art. 187."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9100, "9100 - Tarifa del 5% venta de servicios de almacenamiento y comisiones productos agrícolas. E.T., art. 468-3, num. 1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9101, "9101 - Tarifa del 5% por venta de seguro agropecuario. E.T., art. 468-3, num. 2."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9102, "9102 - Tarifa del 5% por venta de servicios prestados mediante entidades del Num. 1 Art. 19 E.T., con discapacidad. E.T., art. 468-3, num. 4, modificado por L. 1819/2016, art. 186."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9103, "9103 - Tarifa de 5% en venta de servicios de medicina prepagada y pólizas relacionadas. E.T., art. 468-3, num. 3."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9104, "9104 - Tarifa del 5% en la primera venta de unidades de vivienda nueva cuyo valor supere las 26.800 UVT. E.T., art. 468-1, num. 1, adicionado L. 1819/2016, art. 185, num. 1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9105, "9105 - Tarifa del 5% en bienes sujetos a participación o impuesto al consumo de licores, vinos, aperitivos y similares del art. 202 L. 223/1995. E.T., art. 468-1, num. 2, adicionado L. 1819/2016, art. 185, num. 2."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9106, "9106 - Tarifa del 5% en las neveras nuevas para sustitución, sujetas al reglamento técnico de etiquetado (RETIQ). E.T., art. 468-1, num. 3, adicionado L. 1819/2016, art. 185, num.3."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9200, "9200 - Exención de IVA en venta de alcohol carburante. E.T., art. 477, num. 1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9201, "9201 - Exención de IVA en venta de biocombustible. E.T., art. 477, num. 2."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9202, "9202 - Exención de IVA venta de libros y revistas de carácter científico y cultural. E.T., art. 478."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9203, "9203 - Exención por prestación servicios en el país utilizados en el exterior. E.T., art. 481, lit. c."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9204, "9204 - Exención por prestación de servicios turísticos a extranjeros en el territorio colombiano. E.T., art. 481, lit. d."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9205, "9205 - Exención de IVA por cuadernos subpartida 48.20.20.00.00, diarios, publicaciones periódicas, impresos, demás subpartida 49.02. E.T., art. 481, lit. f."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9206, "9206 - Exención de IVA por servicio de conexión estrato 1 y 2. E.T., art. 481, lit. h."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(9207, "9207 - Exención de IVA en venta de municiones y material de guerra y elementos pertenecientes a Fuerzas Militares y Policía Nacional. E.T., art. 477, num. 3."))
                ElseIf Format = 1012 Then
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1110, "1110 - Saldo a 31 de diciembre de las cuentas corrientes y/o ahorro que posea en el país"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1115, "1115 - El valor total del saldo de las cuentas corrientes y/o ahorro poseídas en el exterior"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1200, "1200 - El valor patrimonial de los bonos poseídos a 31 de diciembre"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1201, "1201 - El valor patrimonial de los certificados de depósito poseídos a 31 de diciembre"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1202, "1202 - Valor patrimonial de los títulos poseídos a 31 de diciembre"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1203, "1203 - Valor patrimonial de los derechos fiduciarios poseídos a 31 de diciembre"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1204, "1204 - Valor patrimonial de las demás inversiones poseídas a 31 de diciembre"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(1205, "1205 - Acciones o aportes poseídos en sociedades"))
                ElseIf Format = 1647 Then
                    _FillingConcept.Add(New Tuple(Of Integer, String)(4070, "4070 - Ingresos recibidos para terceros"))
                ElseIf Format = 2275 Then
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8001, "8001 - Ingresos no constitutivos por dividendos y participaciones. E.T., art. 48. (Modificado. L. 1819/2016, art. 2)."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8002, "8002 - Ingresos no constitutivos por componente inflacionario de los rendimientos financieros. E.T. art. 38 al 40."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8005, "8005 - Ingresos no constitutivos por la utilidad en enajenación de acciones. E.T., art. 36-1, incisos 2 y 3."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8006, "8006 - Ingresos no constitutivos por utilidades provenientes de la negociación de derivados. E.T., art. 36-1, inciso 4."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8007, "8007 - Ingresos no constitutivos por capitalizaciones no gravadas a socios o accionistas. E.T. art. 36-3."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8008, "8008 - Ingresos no constitutivos por las indemnizaciones en virtud de seguros de daño. E.T., art. 45."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8009, "8009 - Ingresos no constitutivos por las indemnizaciones por destrucción o renovación de cultivos, y por control de plagas. E.T., art. 46-1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8010, "8010 - Ingresos no constitutivos por los aportes de entidades estatales, sobretasas e impuestos para financiamiento de sistemas de servicio público de transporte masivo de pasajeros. E.T., art. 53."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8011, "8011 - Ingresos no constitutivos percibidos por las organizaciones regionales de televisión y audiovisuales provenientes de la Comisión Nacional de Televisión. L. 488/98, art. 40."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8013, "8013 - Ingresos no constitutivos por la liberación de la reserva de que trata el numeral 12 del artículo Art. 290 E.T., Reserva de que trataba el 130 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8014, "8014 - Ingresos no constitutivos provenientes del Incentivo a la Capitalización Rural, (ICR). E.T., art. 52."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8015, "8015 - Ingresos no constitutivos por la utilidad en la venta de casa o apartamento de habitación. E.T., art. 44."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8016, "8016 - Ingresos no constitutivos por la retribución como recompensa. E.T., art. 42."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8017, "8017 - Ingresos no constitutivos por la utilidad en la enajenación voluntaria de bienes expropiados. L. 388/97, art. 67, par. 2"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8019, "8019 - Ingresos no constitutivos por aportes al sistema general de pensiones. Art. 55 E.T. (Agregado, L. 1819/2016, art. 13)"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8022, "8022 - Ingresos no constitutivos por los aportes del empleador a fondos de cesantías. Art. 56-2 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8023, "8023 - Ingresos no constitutivos por los subsidios y ayudas otorgadas por el programa Agro Ingreso Seguro – AIS e incentivos al almacenamiento y la capitalización rural previstos en la L 101/1993. Art. 57-1 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8025, "8025 - Ingresos no constitutivos por distribución de utilidades por liquidación de sociedades limitadas. E.T., art. 51."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8026, "8026 - Ingresos no constitutivos por donaciones recibidas para partidos, movimientos y campañas políticas. E.T., Art. 47-1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8028, "8028 - Ingresos no constitutivos por la utilidad en procesos de capitalización. L. 789/2002, art. 44."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8029, "8029 - Ingresos no constitutivos recibidos por el contribuyente para ser destinados al desarrollo de proyectos calificados como de carácter científico, tecnológico o de inversión. Art. 57-2 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8030, "8030 - Ingresos no constitutivos recursos administrados por Fogafin. E.T., art. 19-3, inciso 1."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8032, "8032 - Ingresos no constitutivos por gananciales. E.T., art. 47."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8033, "8033 - Ingresos no constitutivos por capitalización de utilidades en ajustes por inflación o componente inflacionario. Art. 50 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8034, "8034 - Ingresos no constitutivos remuneración labores de carácter científico, tecnológico o innovación. E.T., art. 57 -2."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8035, "8035 - Ingresos no constitutivos por apoyos económicos entregados como capital semilla. L. 1429/10, art. 16."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8036, "8036 - Ingresos no constitutivos por recursos recibidos por aportes de la nación a entidades públicas en liquidación. L. 633/2000, art. 77."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8037, "8037 - Ingresos no constitutivos por componente inflacionario o mantenimiento de valor de títulos emitidos en proceso de titularización de cartera hipotecaria. L. 546/1999, art. 16, inciso 4."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8038, "8038 - Ingresos no constitutivos generado en fuentes productoras de algún país de la CAN, diferente de Colombia. Decisión. 578 de 2004, art. 3."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8039, "8039 - Ingresos no constitutivos generado en remuneraciones, honorarios, sueldos, salarios, etc., prestados en otro país de la CAN diferente de Colombia. Decisión 578 de 2004, art. 13."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8040, "8040 - Ingresos no constitutivos por empresas de servicios profesionales, producidos en otro país de la CAN, diferente de Colombia. Decisión. 578 de 2004, art. 14."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8041, "8041 - Ingresos no constitutivos por enajenación de inmuebles. L 9/1989, art. 15, modificado por la L. 3 de 1991, art. 35."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8042, "8042 - Ingresos no constitutivos por dividendos y beneficios distribuidos por la ECE. Art. 893 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8043, "8043 - Ingresos no constitutivos por rentas o ganancias ocasionales por enajenación de acciones o participaciones en la ECE. Inc. 2, Art. 893 E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8044, "8044 - Ingresos no constitutivos por Certificados de Incentivo Forestal. L. 139/1994, art. 8, literal c)."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8045, "8045 - Ingresos no constitutivos por aportes obligatorios al sistema general de salud. Art. 56 E.T. (Agregado, L. 1819/2016, art. 14)"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8046, "8046 - Ingresos no constitutivos por premios obtenidos en virtud del Premio Fiscal que trata el art. 618-1 del E.T."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8047, "8047 - Ingresos no constitutivos por contraprestación por la producción de obras cinematográficas. L. 1556/2012, art. 9 y D.R. 437/2013, art. 8"))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8048, "8048 - Ingresos no constitutivos por donaciones Protocolo Montreal.L.488/1998, art. 32."))
                    _FillingConcept.Add(New Tuple(Of Integer, String)(8049, "8049 - Ingresos no constitutivos por apoyos económicos entregados por el Estado o financiados con recursos públicos. Art. 46, modificado por L. 1819/2016, art. 11."))
                End If
            End If
            Return _FillingConcept
        End Get
    End Property

    ''' <summary>
    ''' Establece los tipos de concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingConceptType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property FillingConceptType As List(Of Tuple(Of Byte, String))
        Get
            If _FillingConceptType Is Nothing Then
                _FillingConceptType = New List(Of Tuple(Of Byte, String))
                If Format = 1001 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Pago o abono en cuenta deducible"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(2, "Pago o abono en cuenta NO deducible"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(3, "IVA mayor valor del costo o gasto, deducible"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(4, "IVA mayor valor del costo o gasto, no deducible"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(5, "Retención en la fuente practicada Renta"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(6, "Retención en la fuente asumida Renta"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(7, "Retención en la fuente practicada IVA Régimen común"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(8, "Retención en la fuente practicada IVA no domiciliados"))
                ElseIf Format = 1003 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Valor acumulado del pago o abono sujeto a Retención en la fuente"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(2, "Retención que le practicaron"))
                ElseIf Format = 1004 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Valor del pago o abono en cuenta"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(2, "Valor del descuento tributario"))
                ElseIf Format = 1005 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Impuesto descontable"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(2, "IVA resultante por devoluciones en ventas anuladas, rescindidas o resueltas"))
                ElseIf Format = 1006 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Impuesto generado"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(2, "IVA recuperado en devoluciones en compras anuladas, rescindidas o resueltas"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(3, "Impuesto al consumo"))
                ElseIf Format = 1007 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Ingresos brutos recibidos"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(2, "Devoluciones, rebajas y descuentos"))
                ElseIf Format = 1008 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Saldo cuentas por cobrar al 31-12"))
                ElseIf Format = 1009 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Saldo cuentas por pagar al 31-12"))
                ElseIf Format = 1010 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Valor patrimonial acciones o aportes al 31-12"))
                ElseIf Format = 1011 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Saldos al 31-12"))
                ElseIf Format = 1012 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Valor al 31-12"))
                ElseIf Format = 1056 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Pago o abono en cuenta"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(2, "IVA mayor valor del costo o gasto"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(3, "Retención en la fuente practicada RENTA"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(4, "Retención en la fuente asumida RENTA"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(5, "Retención en la fuente practicada IVA Régimen Común"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(6, "Retención en la fuente practicada IVA no domiciliados"))
                ElseIf Format = 1647 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Valor total de la operación"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(2, "Valor ingreso reintegrado transferido distribuido al tercero"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(3, "Valor Retención reintegrada transferida distribuida al tercero"))
                ElseIf Format = 2275 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Valor total del ingreso"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(2, "Valor del ingreso no constitutivo de renta ni ganancia ocasional"))
                ElseIf Format = 2276 Then
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(1, "Pagos por salarios"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(2, "Pagos por emolumnetos eclesiásticos"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(3, "Pagos por honorarios"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(4, "Pagos por servicios"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(5, "Pagos por comisiones"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(6, "Pagos por prestaciones sociales"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(7, "Pagos por viáticos"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(8, "Pagos por gastos de representación"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(9, "Pagos por compensaciones trabajo asociado cooperativo"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(10, "Otros pagos"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(11, "Pagos realizados con bonos electrónicos o de papel de servicio, cheques, tarjetas, vales, etc."))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(12, "Cesantías e intereses de censantías efectivamente pagadas, consignadas o reconocidas en el periodo"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(13, "Pensiones de Jubilación, vejez o invalidez"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(14, "Aportes Obligatorios por Salud"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(15, "Aportes obligatorios a fondos de pensiones y solidaridad pensional y Aportes voluntarios al RAIS"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(16, "Aportes voluntarios a fondos de pensiones voluntarias"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(17, "Aportes a cuentas AFC"))
                    _FillingConceptType.Add(New Tuple(Of Byte, String)(18, "Valor de las retenciones en la fuente por pagos de rentas de trabajo o pensiones"))
                End If
            End If
            Return _FillingConceptType
        End Get
    End Property

    ''' <summary>
    ''' Establece las naturalezas a manejar
    ''' </summary>
    ''' <remarks></remarks>
    Private _ListNature As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListNature As List(Of Tuple(Of Byte, String))
        Get
            If _ListNature Is Nothing Then
                _ListNature = New List(Of Tuple(Of Byte, String))
                If Format = 1647 Then
                    _ListNature.Add(New Tuple(Of Byte, String)(1, "Recibe"))
                    _ListNature.Add(New Tuple(Of Byte, String)(2, "Entrega"))
                Else
                    _ListNature.Add(New Tuple(Of Byte, String)(1, "Débito"))
                    _ListNature.Add(New Tuple(Of Byte, String)(2, "Crédito"))
                    _ListNature.Add(New Tuple(Of Byte, String)(3, "Saldo"))
                    _ListNature.Add(New Tuple(Of Byte, String)(4, "Saldo Acumulado"))
                    _ListNature.Add(New Tuple(Of Byte, String)(5, "Base Gravable"))
                    _ListNature.Add(New Tuple(Of Byte, String)(6, "Valor Facturado"))
                End If
            End If
            Return _ListNature
        End Get
    End Property

    ''' <summary>
    ''' Establece las tuplas de donde se obtendrá el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private _ListThirdPartyBy As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property ListThirdPartyBy As List(Of Tuple(Of Byte, String))
        Get
            If _ListThirdPartyBy Is Nothing Then
                _ListThirdPartyBy = New List(Of Tuple(Of Byte, String))
                If Format = 1647 Then
                    _ListThirdPartyBy.Add(New Tuple(Of Byte, String)(2, "Documento origen"))
                Else
                    _ListThirdPartyBy.Add(New Tuple(Of Byte, String)(1, "Documento contable"))
                    _ListThirdPartyBy.Add(New Tuple(Of Byte, String)(2, "Documento origen"))
                End If
            End If
            Return _ListThirdPartyBy
        End Get
    End Property

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Async Sub Deshacer() Implements Base.IcrudBase.Deshacer
        Await CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If Not ValidateControls() OrElse Not ValidateDetail() Then
            Exit Sub
        End If
        Try
            AssigningValues()
            Using Model As New MExogenousFormat(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ExogenousFormat) = Await Model.SaveExogenousFormat(Me._exogenousFormat, Me._idCurrentSequense)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    If _exogenousFormat.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If

                    Me._exogenousFormat = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            NewEntity()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Formato", .FieldName = "Format", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Versión", .FieldName = "Version", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListExogenousFormat
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Activa los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IExogenousFormat.ActionsOnControls
        Set(value As Boolean)
            INDlyExogenousFormat.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleFormat.Enabled = value
            INDtxtVersion.Enabled = value
            INDtxtMinimumValue.Enabled = value
            INDpceAddDetail.Enabled = value
            INDgcDetail.Enabled = value
            INDlyExogenousFormat.EndUpdate()
            If value Then
                INDsleFormat.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

#Region "Details"

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        _exogenousFormatDetail = Nothing

        Concept = Nothing
        ConceptType = Nothing
        MainAccountId = Nothing
        Nature = Nothing
        ThirdPartyBy = Nothing
    End Sub

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As Boolean
        Dim listErrors As New StringBuilder

        If INDlyItemConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If Concept Is Nothing Then
                listErrors.AppendLine("Debe seleccionar un concepto")
            End If
        End If

        If INDlyItemConceptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ConceptType Is Nothing Then
                listErrors.AppendLine("Debe seleccionar un tipo de concepto")
            End If
        End If

        If MainAccountId Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una cuenta contable")
        End If

        If Nature Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una naturaleza")
        End If

        If ThirdPartyBy Is Nothing Then
            listErrors.AppendLine("Debe seleccionar el origen del tercero")
        End If

        If listErrors.Length = 0 AndAlso _listExogenousFormatDetail IsNot Nothing Then
            If _listExogenousFormatDetail.Any(Function(d) d.Concept.Equals(Concept) AndAlso d.ConceptType.Equals(ConceptType) AndAlso d.MainAccountId.Equals(MainAccountId)) Then
                listErrors.AppendLine("El detalle ya se encuentra agregado")
            End If
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Agrega el detalle a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddNewDetail()
        If Format = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un formato."
            Exit Sub
        End If

        If ValidateControlsPopup() = True Then
            AssigningValuesDetail()

            If _listExogenousFormatDetail Is Nothing Then
                _listExogenousFormatDetail = New List(Of ExogenousFormatDetail)
            End If
            _listExogenousFormatDetail.Add(_exogenousFormatDetail)
            If _exogenousFormat.Id > 0 Then
                _exogenousFormat.MarkAsModified()
            End If
            INDgcDetail.DataSource = Nothing
            INDgcDetail.DataSource = _listExogenousFormatDetail

            INDsleFormat.Properties.ReadOnly = True
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado con éxito"
            CleanControlsPopup()

            If INDlyItemConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleConcept.Focus()
            ElseIf INDlyItemConceptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleConceptType.Focus()
            Else
                INDsleMainAccount.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Crea el objeto para el listado de detalles de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValuesDetail()
        _exogenousFormatDetail = New ExogenousFormatDetail
        With _exogenousFormatDetail
            .Concept = Concept
            .ConceptType = ConceptType
            .MainAccountId = MainAccountId
            .MainAccountNumberName = INDsleMainAccount.Text
            .Nature = Nature
            .ThirdPartyBy = ThirdPartyBy
        End With
    End Sub

    ''' <summary>
    ''' Elimina el detalle de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim dld As ExogenousFormatDetail = CType(INDgvDetails.GetFocusedRow, ExogenousFormatDetail)
        If dld.Id <> 0 Then
            If _listDeleteExogenousFormatDetail Is Nothing Then
                _listDeleteExogenousFormatDetail = New List(Of ExogenousFormatDetail)
            End If
            dld.MarkAsDeleted()
            _listDeleteExogenousFormatDetail.Add(dld)
        End If
        _listExogenousFormatDetail.Remove(dld)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = _listExogenousFormatDetail

        If Not _listExogenousFormatDetail.Any() Then
            INDsleFormat.Properties.ReadOnly = False
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CleanControls() As Task
        INDlyExogenousFormat.BeginUpdate()
        Await DeleteBlockedRecord()

        INDbtnCode.Text = String.Empty
        INDsleFormat.EditValue = Nothing
        INDsleFormat.Properties.ReadOnly = False
        INDtxtVersion.EditValue = Nothing
        INDtxtMinimumValue.EditValue = Nothing
        INDgcDetail.DataSource = Nothing

        CleanControlsPopup()

        _doc = Nothing
        _exogenousFormat = Nothing
        _listExogenousFormatDetail = Nothing
        _listDeleteExogenousFormatDetail = Nothing

        ActionsOnControls = False
        Me.BarraBotones.StatusRecord = -1
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyExogenousFormat.EndUpdate()
    End Function

    ''' <summary>
    ''' Función para validar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As Boolean
        Dim listErrors As New StringBuilder

        If Not MinimumValue >= 0 Then
            listErrors.AppendLine("Debe establecer un valor mínimo válido.")
        End If

        If Not (_listExogenousFormatDetail IsNot Nothing AndAlso _listExogenousFormatDetail.Any()) Then
            listErrors.AppendLine("Se debe agregar minimo un detalle.")
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _exogenousFormat
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Format = Format
            .Version = Version
            .MinimumValue = MinimumValue

            If _listExogenousFormatDetail IsNot Nothing Then
                For Each itemDetail As ExogenousFormatDetail In _listExogenousFormatDetail
                    .ExogenousFormatDetail.Add(itemDetail)
                Next
            End If
            If _listDeleteExogenousFormatDetail IsNot Nothing Then
                For Each itemDetail As ExogenousFormatDetail In _listDeleteExogenousFormatDetail
                    .ExogenousFormatDetail.Add(itemDetail)
                Next
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Sub NewEntity()
        _exogenousFormat = New ExogenousFormat() With {.Status = True}
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.GeneralLedgerSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequense.GeneralLedgerSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Me._sequense.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MRetentionConcept(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MExogenousFormat(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlyExogenousFormat.BeginUpdate()
                    Dim resultOperation = Await Model.GetExogenousFormatByCode(INDbtnCode.Text.Trim)
                    If resultOperation.StateResult Then
                        _exogenousFormat = resultOperation.ObjectEmbbeded
                        If _exogenousFormat IsNot Nothing AndAlso _exogenousFormat.Id > 0 Then
                            Me.BarraBotones.StatusRecordVisible = True
                            _blockRecord = Await Model.GetBlockRecordAccounting(CStr(Me.Tag), CStr(_exogenousFormat.Id))
                            With _exogenousFormat
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                Format = .Format
                                INDsleFormat.Properties.ReadOnly = True
                                Version = .Version
                                MinimumValue = .MinimumValue
                                _listExogenousFormatDetail = .ExogenousFormatDetail.ToList()
                                INDgcDetail.DataSource = _listExogenousFormatDetail

                                Me.BarraBotones.StatusRecord = If(.Status, eActionsStatusRecords.Active, eActionsStatusRecords.Inactive)
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._exogenousFormat.Code)
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await Model.SaveBlockRecordAccounting(
                                        New BlockRecordGeneralLedger With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                            .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _exogenousFormat.Id})
                                        ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(_exogenousFormat.Id, Me.Tag.ToString(), Nothing, GetType(ExogenousFormat).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

                            AsyncLoader(False)
                            ActionsOnControls = True
                        Else
                            AsyncLoader(False)
                            If Me._sequense.IsManual Then
                                Me.NewEntity()
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                                Code = String.Empty
                                INDbtnCode.Focus()
                            End If
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = resultOperation.Message
                    End If
                    INDlyExogenousFormat.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me._exogenousFormat.Code) Then
            Try
                Using model As New MExogenousFormat(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not _exogenousFormat.Status
                    Dim result As ActionResult(Of ExogenousFormat) = Await model.ChangeState(Me._exogenousFormat.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._exogenousFormat = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent"), Me._exogenousFormat.Code, String.Format("{0} ({1})", Me._exogenousFormat.Format, Me._exogenousFormat.Version)),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._exogenousFormat.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle"), Me._exogenousFormat.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent"), Me._exogenousFormat.Code, String.Format("{0} ({1})", Me._exogenousFormat.Format, Me._exogenousFormat.Version))
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle"), Me._exogenousFormat.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MExogenousFormat(CStr(Me.Tag))
                Await model.DeleteBlockRecordAccounting(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._exogenousFormat IsNot Nothing AndAlso Me._exogenousFormat.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmExogenousFormat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyExogenousFormat, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        _presenter = New PExogenousFormat(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        _idOperativeUnit = BarraBotones.OperatingUnitValue

        IndigoGridView1.SetListAcction(INDgvDetails, New List(Of eAcciones)() From {{eAcciones.Remove}})
        IndigoGridControl1.RefreshGrid(INDgcDetail)

        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _blockRecord = Nothing
        _exogenousFormat = Nothing
        _exogenousFormatDetail = Nothing
        _listExogenousFormatDetail = Nothing
        _listDeleteExogenousFormatDetail = Nothing
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmExogenousFormat_Activated(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleFormat.Properties.DataSource = ListFormat
        '******************************
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmExogenousFormat_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    NewEntity()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape al control del valor mínimo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtMinimumValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtMinimumValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDpceAddDetail.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceAddDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceAddDetail.ShowPopup()
            If INDlyItemConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleConcept.Focus()
            ElseIf INDlyItemConceptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleConceptType.Focus()
            Else
                INDsleMainAccount.Focus()
            End If
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccount.QueryPopUp
        If INDsleMainAccount.Properties.DataSource Is Nothing Then
            _presenter.InitializeMainAccount()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddNewDetail()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton del control de la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeMainAccount()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento para cargar la versión de acuerdo al formato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFormat_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFormat.EditValueChanged
        Me._FillingConcept = Nothing
        Me._FillingConceptType = Nothing
        Me._ListNature = Nothing
        Me._ListThirdPartyBy = Nothing

        '******************************
        INDsleConcept.Properties.DataSource = Me.FillingConcept
        INDsleConceptType.Properties.DataSource = Me.FillingConceptType
        INDsleNature.Properties.DataSource = ListNature
        INDsleThirdPartyBy.Properties.DataSource = ListThirdPartyBy
        '******************************
        INDrepSleConcept.DataSource = Me.FillingConcept
        INDrepSleConceptType.DataSource = Me.FillingConceptType
        INDrepSleNature.DataSource = ListNature
        INDrepSleThirdPartyBy.DataSource = ListThirdPartyBy
        CleanControlsPopup()

        Select Case Format
            Case 1001
                Version = 10
            Case 1003
                Version = 7
            Case 1004
                Version = 7
            Case 1005
                Version = 7
            Case 1006
                Version = 8
            Case 1007
                Version = 9
            Case 1008
                Version = 7
            Case 1009
                Version = 7
            Case 1010
                Version = 8
            Case 1011
                Version = 6
            Case 1012
                Version = 7
            Case 1056
                Version = 10
            Case 1647
                Version = 2
            Case 2275
                Version = 1
            Case 2276
                Version = 2
            Case 5253
                Version = 1
            Case Else
                INDtxtVersion.Text = String.Empty
        End Select

        INDlyItemConcept.HideControl(Not FillingConcept.Any())
        INDlyItemConceptType.HideControl(Not FillingConceptType.Any())

        INDcolDetail_Concept.HideControl(Not FillingConcept.Any(), 0)
        INDcolDetail_ConceptType.HideControl(Not FillingConceptType.Any(), 1)
    End Sub

#End Region

#Region "ContextMenuGridControl"

    ''' <summary>
    ''' Evento que llama al método DeleteDetail
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        DeleteDetail()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.GeneralLedgerSequenceDetail IsNot Nothing Then
                If Not Me._sequense.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class