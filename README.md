# WebApiTestForSantander
WebApiTestForSantander


To start the app localy please use Visual Studio or Visual Studio Code. 
Following steps for MS Visual Studio
 1) select "open solution" and choose "WebApiTestForSantander.sln" 
 2) tap F5 to start app locally for Debug conf. In that case Swagger will opens automatically and you'll be able to chech both of created endpoint.
  a) first one returns list of all Stories Ids
  b) second one returns the number of Stories items with details. it returns Json with selected by user items sorted by score in a descending order.

! Known issue: 
 due to lack of time, the conversion of the date format from the input format to the required output format was not implemented

possible ways to improve

1) Add authentication and authorisation (1d)
	a) to develop secure local storage to store hash of user values(0.5d)
2) After performance investigation applied approach to use parallel requests for number of required items can be changed or can be applied invoking of cancellation token to abort long request(about 1d)
3) Can be added static mapper to simplify explicit mapping of incoming values to outcoming (0.5d)


