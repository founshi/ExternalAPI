# ExternalAPI
WebService方法被
系统通过反射,调用C#写的dll文件,来实现方法的调用.
给外部调用只有一个方法CallBackEnd,传入的参数:APISerialKey(这个值是方法的唯一值),inParamXML是通过反射出来的方法调用时的参数
系统使用sqlSugar,sqlite数据库(可以切换到oracle,sqlserver)
