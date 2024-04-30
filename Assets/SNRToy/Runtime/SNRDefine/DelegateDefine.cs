


public delegate void DelRtnVoid1P<T>(T t);
public delegate void DelRtnVoid2P<T1, T2>(T1 t1, T2 t2);
public delegate void DelRtnVoid3P<T1, T2, T3>(T1 t1, T2 t2, T3 t3);


public delegate void DelRtnVoid1More<T>(params T[] ts);
public delegate void DelRtnVoid2More<T1, T2>(T1 t1, params T2[] t2s);