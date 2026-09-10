Imports Domain.Base
Imports Domain.Security
Imports Infrastructure.CrossCutting.Base

Public Class FactoryQueue
	Implements IFactoryQueue, Inject

	Private ReadOnly _containersRepository As IContainersRepository
	Private _architectureQueue As Integer?

	Public Sub New(containersRepository As IContainersRepository)
		_containersRepository = containersRepository
	End Sub

	Public Function CreateQueue() As IIndigoQueue Implements IFactoryQueue.CreateQueue
		_architectureQueue = _containersRepository.getContainersByTransactionalContainer(ServerSessionValues.Current.CurrentContainer).ArchitectureQueue

		Select Case _architectureQueue
			Case 1 'RabbitMQ
				Dim urlQueue = _containersRepository.getContainersByTransactionalContainer(ServerSessionValues.Current.CurrentContainer).UrlQueue
				Dim queueName = _containersRepository.getContainersByTransactionalContainer(ServerSessionValues.Current.CurrentContainer).NameQueue
				Return New RabbitMQIndigoQueue(urlQueue, queueName)

			Case 2 'Azure Service Bus
				Return New AzureServiceBusIndigoQueue
			Case Else
				Return New AzureServiceBusIndigoQueue
		End Select
	End Function

End Class
