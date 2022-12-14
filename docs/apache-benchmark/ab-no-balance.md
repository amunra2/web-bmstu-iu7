$ ab -c 10 -n 1000 http://localhost:8080/api/v1/servers

This is ApacheBench, Version 2.3 <$Revision: 1879490 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        nginx/1.23.2
Server Hostname:        localhost
Server Port:            8080

Document Path:          /api/v1/servers
Document Length:        2 bytes

Concurrency Level:      10
Time taken for tests:   2.689 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      146000 bytes
HTML transferred:       2000 bytes
Requests per second:    371.91 [#/sec] (mean)
Time per request:       26.888 [ms] (mean)
Time per request:       2.689 [ms] (mean, across all concurrent requests)
Transfer rate:          53.03 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.0      0       1
Processing:    10   26  11.4     25     152
Waiting:        9   25  11.3     23     149
Total:         10   26  11.4     25     152

Percentage of the requests served within a certain time (ms)
  50%     25
  66%     28
  75%     29
  80%     30
  90%     33
  95%     35
  98%     40
  99%     46
 100%    152 (longest request)