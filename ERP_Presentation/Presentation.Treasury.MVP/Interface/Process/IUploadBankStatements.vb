#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports DevExpress.Data.Linq

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IUploadBankStatements
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece el Codigo de la Carga de extractos bancarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el datasource de las cuentas bancarias
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Property BankAccountDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Id de la Cuenta Bancaria
    ''' </summary>
    ''' <returns></returns>
    Property IdEntityBankAccount As Integer?


#End Region

End Interface
