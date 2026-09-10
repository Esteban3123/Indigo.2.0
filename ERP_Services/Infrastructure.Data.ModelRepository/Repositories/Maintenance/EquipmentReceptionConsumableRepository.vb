'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 03-03-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region
Public Class EquipmentReceptionConsumableRepository
    Inherits GenericRepository(Of EquipmentReceptionConsumable)
    Implements IEquipmentReceptionConsumableRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
End Class
