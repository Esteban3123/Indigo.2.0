'***********************************************************************
' Assembly         : Presentacion.Corporation.MVP
' Author           : Jose Luis Rojas
' Created          : 07-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IBank
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo de los bancos
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Esta propiedad contiene el Id del tercero
    ''' </summary>
    Property ThirdPartyId As Integer?

    ''' <summary>
    ''' Esta propiedad contiene el nombre de los bancos
    ''' </summary>
    Property NameBank As String

    ''' <summary>
    ''' Esta propiedad contiene el codigo ACH de los bancos
    ''' </summary>
    Property AchCodeBank As String

    ''' <summary>
    ''' Esta propiedad contiene el Código del Archivo Plano del Banco
    ''' </summary>
    Property BankFileCode As String

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad que contiene el Registro Cuenta Bancaria
    ''' </summary>
    Property BankAccountRegistration As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Propiedad solo escritura que contiene el datasource de los terceros
    ''' </summary>
    WriteOnly Property ThirdPartyDataSource As XPInstantFeedbackSource
    ReadOnly Property MyTag As Object
    Property Sequence As TreasurySequence

    ''' <summary>
    '''  obtiene o establece los conceptos bancaria
    ''' </summary>    
    Property BankConceptsXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    '''  obtiene o establece la nota de concepto bancaria
    ''' </summary>    
    Property NoteConceptsXPO As DevExpress.Xpo.XPInstantFeedbackSource
#End Region

End Interface

