


public delegate RT Delegate1P<RT, T>(T t);
public delegate RT Delegate2P<RT, T1, T2>(T1 t1, T2 t2);
public delegate RT Delegate3P<RT, T1, T2, T3>(T1 t1, T2 t2, T3 t3);


public delegate RT Delegate1More<RT, T>(params T[] ts);
public delegate RT Delegate2More<RT, T1, T2>(T1 t1, params T2[] t2s);