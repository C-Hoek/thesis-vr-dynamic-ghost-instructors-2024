import math
import numpy as np

from dtaidistance import dtw_ndim


def calculate_euclidean_distance(mean_shifted_student, mean_shifted_ghost):
    """
    Calculates the pairwise Euclidean distance between the two input arrays.
    :param mean_shifted_student: (time, xs, ys, zs)
    :param mean_shifted_ghost: (time, xs, ys, zs)
    :return: An array of distances of the same length as the two input arrays.
    """
    distance = []

    _, xs1, ys1, zs1 = mean_shifted_student
    _, xs2, ys2, zs2 = mean_shifted_ghost
    for i in range(0, len(xs1)):
        difference = (xs1[i] - xs2[i], ys1[i] - ys2[i], zs1[i] - zs2[i])
        distance.append(np.linalg.norm(difference))

    return distance


def calculate_dtw_distance(mean_shifted_student, mean_shifted_ghost):
    """
    Calculates the DTW distance between the two input arrays.
    :param mean_shifted_student: (time, xs, ys, zs)
    :param mean_shifted_ghost: (time, xs, ys, zs)
    :return: A value representative of the difference between the two time series.
    """
    t, xs1, ys1, zs1 = mean_shifted_student
    _, xs2, ys2, zs2 = mean_shifted_ghost

    dtw_student = np.empty(shape=(len(t), 3))
    dtw_ghost = np.empty(shape=(len(t), 3))
    for i in range(len(xs1)):
        dtw_student[i, 0] = xs1[i]
        dtw_student[i, 1] = ys1[i]
        dtw_student[i, 2] = zs1[i]

        dtw_ghost[i, 0] = xs2[i]
        dtw_ghost[i, 1] = ys2[i]
        dtw_ghost[i, 2] = zs2[i]

    d = dtw_ndim.distance(dtw_student, dtw_ghost)

    return d


def calculate_distance_matrix(mean_shifted_student, mean_shifted_ghost):
    """
    Returns the distance matrix constructed from pairwise distance of the input arrays.
    :param mean_shifted_student: (time, xs, ys, zs)
    :param mean_shifted_ghost: (time, xs, ys, zs)
    :return: The distance matrix.
    """
    t, xs1, ys1, zs1 = mean_shifted_student
    _, xs2, ys2, zs2 = mean_shifted_ghost

    n = len(xs1)
    m = len(xs2)

    dist_mat = np.zeros((n, m))

    for i in range(n):
        for j in range(m):
            s = np.array([xs1[i], ys1[i], zs1[i]])
            g = np.array([xs2[j], ys2[j], zs2[j]])
            dist_mat[i, j] = np.linalg.norm(s - g)

    return dist_mat


def calculate_dtw_distance_manual(mean_shifted_student, mean_shifted_ghost):
    """
    Calculates the DTW distance between the two input arrays.
    :param mean_shifted_student: (time, xs, ys, zs)
    :param mean_shifted_ghost: (time, xs, ys, zs)
    :return: A value representative of the difference between the two time series.
    """
    n = len(mean_shifted_student[1])
    m = len(mean_shifted_ghost[1])

    # Obtain the distance matrix.
    dist_mat = calculate_distance_matrix(mean_shifted_student, mean_shifted_ghost)
    cost_mat = np.zeros((n + 1, m + 1))

    # Initialise the infinity rows.
    for i in range(n + 1):
        if i == 0:
            continue
        cost_mat[i, 0] = np.inf

    for j in range(m + 1):
        if j == 0:
            continue
        cost_mat[0, j] = np.inf

    # Run the DTW algorithm without any optimisation...
    for i in range(1, n + 1):
        for j in range(1, m + 1):
            cost = [cost_mat[i - 1, j - 1], cost_mat[i - 1, j], cost_mat[i, j - 1]]
            cost_mat[i, j] = (
                    dist_mat[i - 1, j - 1] + cost[np.argmin(cost)])

    return np.sqrt(cost_mat[n, m])


def mean_shift_data(pointer_pos):
    """
    This method shifts the positions by the mean position.
    It also splits the data in time, x, y, and z coordinates.
    :param pointer_pos: The (time,vector3) list.
    :return: Time, mean-shifted x, y, and z coordinates.
    """
    ts = []
    xs = []
    ys = []
    zs = []

    for t, vec in pointer_pos:
        ts.append(t)
        xs.append(vec.x)
        ys.append(vec.y)
        zs.append(vec.z)

    ts = np.array(ts)
    xs = np.array(xs)
    ys = np.array(ys)
    zs = np.array(zs)

    return ts, xs - np.mean(xs), ys - np.mean(ys), zs - np.mean(zs)


def calculate_alpha(errors, min_alpha, max_alpha):
    """
    This method is a modified replication of the C# script DynamicTransparencySetting's TargetGhostTransparency().
    The modification is to calculate the alpha for a list of errors rather than for a single error.
    :param errors: [float] A list of error values for which the corresponding transparency should be calculated.
    :param min_alpha: float. The minimum transparency that is allowed.
    :param max_alpha: float. The maximum transparency that is allowed.
    :return: [float]. A list of the transparency (alpha) values that the ghost should display for the passed list of errors.
    """
    error_threshold = float(0.3)
    alphas = []

    for error in errors:
        transparency_modifier = compute_modifier(error, error_threshold)

        alpha = min_alpha + transparency_modifier * (max_alpha - min_alpha)
        alphas.append(alpha)

    return alphas


def compute_modifier(error, error_threshold):
    """
    This method is a Python replica of the C# script DynamicTransparencySetting's ComputeTransparencyModifier().
    :param error: The calculated position error between the student and the Ghost avatar.
    :param error_threshold: The error below which the transparency will be set to 0.
    :return: The modifier used to linearly interpolate between the minimum and maximum transparency.
    """
    if error <= error_threshold:
        return float(0)

    exponential_error = math.exp(error)

    modifier = (exponential_error - float(1)) / float(math.e)
    return modifier
