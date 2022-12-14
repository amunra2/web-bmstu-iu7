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
Time taken for tests:   3.123 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      146000 bytes
HTML transferred:       2000 bytes
Requests per second:    320.20 [#/sec] (mean)
Time per request:       31.231 [ms] (mean)
Time per request:       3.123 [ms] (mean, across all concurrent requests)
Transfer rate:          45.65 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.1      0       1
Processing:    11   31   8.8     29      64
Waiting:        8   29   8.6     28      60
Total:         11   31   8.8     29      64

Percentage of the requests served within a certain time (ms)
  50%     29
  66%     33
  75%     35
  80%     37
  90%     43
  95%     48
  98%     54
  99%     58
 100%     64 (longest request)